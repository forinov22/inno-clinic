using Microsoft.AspNetCore.Http;

namespace Innowise.Offices.Contracts.Offices;

public record CreateOfficeRequest(
    IFormFile Photo,
    string City,
    string Street,
    string HouseNumber,
    string OfficeNumber,
    string RegistryPhoneNumber,
    string OfficeStatus);