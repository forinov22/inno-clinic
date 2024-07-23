namespace Appointments.Application.Doctors.Common;

public class TimeSlotResult(TimeOnly start, TimeOnly end, bool isAvailable = true)
{
    public TimeOnly Start { get; set; } = start;
    public TimeOnly End { get; set; } = end;
    public bool IsAvailable { get; set; } = isAvailable;
}