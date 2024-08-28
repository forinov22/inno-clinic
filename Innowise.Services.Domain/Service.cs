namespace Services.Domain;

public class Service
{
    public Guid Id { get; set; }
    public Guid ServiceCategoryId { get; set; }
    public ServiceCategory ServiceCategory { get; set; } = null!;
    public string ServiceName { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public Guid SpecializationId { get; set; }
    public Specialization Specialization { get; set; } = null!;
    public bool IsActive { get; set; }
}