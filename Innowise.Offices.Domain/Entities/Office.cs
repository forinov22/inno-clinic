namespace Innowise.Offices.Domain.Entities;

public class Office
{
    public Guid Id { get; set; }
    public Address Address { get; set; } = null!;
    public string RegistryPhoneNumber { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public OfficeStatus OfficeStatus { get; set; }
}