namespace Services.Domain;

public class ServiceCategory
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int TimeSlotSize { get; set; }
}