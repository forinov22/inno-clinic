namespace Appointments.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; } = string.Empty;
    public Guid ServiceCategoryId { get; set; }
    public string ServiceCategoryName { get; set; } = string.Empty;
    public int TimeSlotSize { get; set; }
}