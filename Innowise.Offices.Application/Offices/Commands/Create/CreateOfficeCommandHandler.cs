using Innowise.Offices.Application.Extensions;
using Innowise.Offices.Application.Interfaces;
using Innowise.Offices.Application.Interfaces.HttpClients.Documents;
using Innowise.Offices.Application.Offices.Common;
using Innowise.Offices.Application.Offices.Exceptions;
using Innowise.Offices.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Innowise.Offices.Application.Offices.Commands.Create;

public class CreateOfficeCommandHandler(
    IUnitOfWork unitOfWork,
    HttpClient httpClient,
    IConfiguration configuration,
    IDocumentHttpClient documentHttpClient)
    : IRequestHandler<CreateOfficeCommand, OfficeResult>
{
    public HttpClient HttpClient { get; } = httpClient;

    public async Task<OfficeResult> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<OfficeStatus>(request.OfficeStatus, out var officeStatus))
            throw new InvalidOfficeStatusException();

        var documentsApiUrl = configuration["ServiceUrl:DocumentsService:BaseUrl"];
        var photoId = await documentHttpClient.UploadPhotoAsync(request.Photo);

        var office = new Office
        {
            Address = new Address()
            {
                City = request.City,
                HouseNumber = request.HouseNumber,
                OfficeNumber = request.OfficeNumber,
                Street = request.Street
            },
            RegistryPhoneNumber = request.RegistryPhoneNumber,
            OfficeStatus = officeStatus,
            PhotoUrl =
                $"{documentsApiUrl}/api/blobs/{photoId}"
        };

        unitOfWork.OfficeRepository.Add(office);
        await unitOfWork.SaveAllAsync();

        return office.ToOfficeResult();
    }
}