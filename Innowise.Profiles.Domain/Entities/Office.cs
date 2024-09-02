namespace Profiles.Domain.Entities;

public class Office
{
    public Guid Id { get; set; }
    public Address Address { get; set; }
    public string RegistryPhoneNumber { get; set; }
    public string PhotoUrl { get; set; }
    public OfficeStatus OfficeStatus { get; set; }
}