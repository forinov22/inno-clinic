namespace Innowise.Offices.Application.Offices.Common;

public record OfficeResult(
    Guid Id,
    string PhotoUrl,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    string RegistryPhoneNumber,
    string OfficeStatus);