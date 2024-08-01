using Microsoft.AspNetCore.Http;

namespace Innowise.Offices.Contracts.Offices;

public record EditOfficeRequest(
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    string RegistryPhoneNumber,
    string OfficeStatus);