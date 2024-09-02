using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Configuration;
using Profiles.Application.Extensions;
using Profiles.Application.Interfaces;
using Profiles.Application.Receptionists.Common;
using Profiles.Application.Shared;
using Profiles.Domain.Entities;

namespace Profiles.Application.Receptionists.Commands.Create;

internal class CreateReceptionistProfileCommandHandler(
    IUnitOfWork unitOfWork,
    HttpClient httpClient,
    IConfiguration configuration) : IRequestHandler<CreateReceptionistProfileCommand, ReceptionistResult>
{
    public async Task<ReceptionistResult> Handle(CreateReceptionistProfileCommand request,
                                                 CancellationToken cancellationToken)
    {
        var account = await CreateAccountAsync(request.Email, httpClient, configuration, cancellationToken);

        var receptionist = new Receptionist()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            WorkerStatus = Enum.Parse<WorkerStatus>(request.WorkerStatus),
            AccountId = account.AccountId,
        };

        unitOfWork.ReceptionistRepository.Add(receptionist);
        await unitOfWork.SaveAllAsync();

        return receptionist.ToReceptionistResult();
    }

    private static async Task<AccountResult> CreateAccountAsync(string email,
                                                                HttpClient httpClient,
                                                                IConfiguration configuration,
                                                                CancellationToken cancellationToken)
    {
        var authApiUrl = configuration["Urls:Auth"]
                      ?? throw new Exception("Failed to get auth api url");

        var jsonRequest = JsonSerializer.Serialize(new { email, Role = "Receptionist" });
        var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{authApiUrl}/api/auth/stuff/sign-up", requestContent, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to fetch account");
        }

        return await response.Content.ReadFromJsonAsync<AccountResult>(cancellationToken)
            ?? throw new Exception("Failed to deserialize account");
    }
}