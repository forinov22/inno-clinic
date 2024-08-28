namespace Services.Domain;

public class Specialization
{
    public Guid Id { get; set; }
    public string SpecializationName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}