namespace Appointments.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
    public Guid ServiceCategoryId { get; set; }
    public string ServiceCategoryName { get; set; }
    public int TimeSlotSize { get; set; }
    //public Guid ExternalId { get; set; }
    //public bool SpecializationIsActive { get; set; }
}