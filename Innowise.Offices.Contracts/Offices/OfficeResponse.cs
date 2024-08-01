using System.Text.Json.Serialization;

namespace Innowise.Offices.Contracts.Offices;

public record OfficeResponse(
    [property:JsonPropertyName("id")]
    Guid Id,
    [property:JsonPropertyName("photoUrl")]
    string PhotoUrl,
    [property:JsonPropertyName("city")]
    string City,
    [property:JsonPropertyName("street")]
    string Street,
    [property:JsonPropertyName("houseNumber")]
    string HouseNumber,
    [property:JsonPropertyName("officeNumber")]
    string OfficeNumber,
    [property:JsonPropertyName("registryPhoneNumber")]
    string RegistryPhoneNumber,
    [property:JsonPropertyName("officeStatus")]
    string OfficeStatus);