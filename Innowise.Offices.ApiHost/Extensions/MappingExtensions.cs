using Innowise.Offices.Application.Offices.Commands.Create;
using Innowise.Offices.Application.Offices.Commands.Edit;
using Innowise.Offices.Application.Offices.Common;
using Innowise.Offices.Contracts.Offices;

namespace Innowise.Offices.ApiHost.Extensions;

public static class MappingExtensions
{
    public static OfficeResponse ToOfficeResponse(this OfficeResult officeResult)
    {
        return new OfficeResponse(officeResult.Id, officeResult.PhotoUrl, officeResult.City,
                                  officeResult.Street, officeResult.HouseNumber,
                                  officeResult.OfficeNumber, officeResult.RegistryPhoneNumber,
                                  officeResult.RegistryPhoneNumber);
    }

    public static CreateOfficeCommand ToCreateOfficeCommand(this CreateOfficeRequest createOfficeRequest)
    {
        return new CreateOfficeCommand(createOfficeRequest.Photo, createOfficeRequest.City,
                                       createOfficeRequest.Street,
                                       createOfficeRequest.HouseNumber, createOfficeRequest.OfficeNumber,
                                       createOfficeRequest.RegistryPhoneNumber,
                                       createOfficeRequest.OfficeStatus);
    }

    public static EditOfficeCommand ToEditOfficeCommand(this EditOfficeRequest editOfficeRequest,
        Guid officeId)
    {
        return new EditOfficeCommand(officeId, editOfficeRequest.City,
                                     editOfficeRequest.Street, editOfficeRequest.HouseNumber,
                                     editOfficeRequest.OfficeNumber, editOfficeRequest.RegistryPhoneNumber,
                                     editOfficeRequest.OfficeStatus);
    }
}