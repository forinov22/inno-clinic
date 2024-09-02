using MediatR;
using Profiles.Application.Doctors.Common;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using Innowise.Common.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Profiles.Application.Doctors.Exceptions;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;
using Profiles.Domain.Entities;
using Profiles.Application.Shared;

namespace Profiles.Application.Doctors.Commands.Create;

internal class CreateDoctorProfileCommandHandler(
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint,
    IConfiguration configuration,
    HttpClient httpClient) : IRequestHandler<CreateDoctorProfileCommand, DoctorResult>
{
    public async Task<DoctorResult> Handle(CreateDoctorProfileCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<WorkerStatus>(request.WorkerStatus, out var workerStatus))
        {
            throw new InvalidDoctorStatusException();
        }

        var office = await unitOfWork.OfficeRepository.GetOfficeByIdAsync(request.OfficeId);
        if (office is null)
        {
            throw new OfficeNotFoundException();
        }

        var specialization = await unitOfWork.SpecializationRepository.GetSpecializationByIdAsync(request.SpecializationId);
        if (specialization is null)
        {
            throw new SpecializationNotFoundException();
        }

        var accountTask = CreateAccountAsync(request.Email, httpClient, configuration, cancellationToken);
        var photoUrlTask = UploadPhotoAsync(request.Photo, httpClient, configuration, cancellationToken);

        await Task.WhenAll(accountTask, photoUrlTask);

        var doctor = new Doctor()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
            CareerStartYear = request.CareerStartYear,
            WorkerStatus = Enum.Parse<WorkerStatus>(request.WorkerStatus),
            PhotoUrl = photoUrlTask.Result,
            AccountId = accountTask.Result.AccountId,
            OfficeId = office.Id,
            OfficeAddress = office.Address,
            SpecializationId = specialization.Id,
            SpecializationName = specialization.SpecializationName
        };

        unitOfWork.DoctorRepository.Add(doctor);
        await unitOfWork.SaveAllAsync();

        await publishEndpoint.Publish(new DoctorProfileCreated()
        {
            ProfileId = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            MiddleName = doctor.MiddleName
        }, cancellationToken);

        return doctor.ToDoctorResult();
    }

    private static async Task<AccountResult> CreateAccountAsync(string email,
                                                           HttpClient httpClient,
                                                           IConfiguration configuration,
                                                           CancellationToken cancellationToken)
    {
        var authApiUrl = configuration["Urls:Auth"]
                      ?? throw new Exception("Failed to get auth api url");

        var jsonRequest = JsonSerializer.Serialize(new { email, Role = "Doctor" });
        var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{authApiUrl}/api/auth/stuff/sign-up", requestContent, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create account");
        }

        return await response.Content.ReadFromJsonAsync<AccountResult>(cancellationToken)
            ?? throw new Exception("Failed to deserialize account");
    }

    private static async Task<string> UploadPhotoAsync(IFormFile photo,
                                                  HttpClient httpClient,
                                                  IConfiguration configuration,
                                                  CancellationToken cancellationToken)
    {
        var documentsApiUrl = configuration["Urls:Documents"]
                           ?? throw new Exception("Failed to get documents api url");

        var response = await httpClient.PostAsync($"{documentsApiUrl}/api/blobs", new MultipartFormDataContent
        {
            { new StreamContent(photo.OpenReadStream()), "file", photo.FileName }
        }, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to upload photo");
        }

        var photoId = await response.Content.ReadAsStringAsync(cancellationToken)
                   ?? throw new Exception("Failed to get photo id");
        return $"{documentsApiUrl}/api/blobs/{photoId}";
    }
}