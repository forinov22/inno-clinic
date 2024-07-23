namespace Innowise.Common.Messages;

public class DoctorProfileCreated
{
    public Guid ProfileId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
}