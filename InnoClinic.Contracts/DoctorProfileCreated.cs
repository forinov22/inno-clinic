namespace InnoClinic.Contracts;

public class DoctorProfileCreated
{
    public Guid ProfileId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
}