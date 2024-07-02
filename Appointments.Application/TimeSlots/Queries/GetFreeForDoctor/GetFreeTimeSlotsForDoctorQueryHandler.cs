using Appointments.Application.Interfaces;
using Appointments.Application.TimeSlots.Common;
using MediatR;

namespace Appointments.Application.TimeSlots.Queries.GetFreeForDoctor;

public class GetFreeTimeSlotsForDoctorQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetFreeTimeSlotsForDoctorQuery, IEnumerable<TimeSlotResult>>
{
    private static readonly TimeOnly WorkingHoursStart = new(9, 0); // 09:00
    private static readonly TimeOnly WorkingHoursEnd = new(17, 0); // 17:00
    private static readonly TimeSpan TimeSlotDuration = new(0, 0, 10, 0); // 10 min

    public async Task<IEnumerable<TimeSlotResult>> Handle(GetFreeTimeSlotsForDoctorQuery request,
                                                          CancellationToken cancellationToken)
    {
        var date = request.Date ?? DateTime.Now;
        var appointments = await unitOfWork.AppointmentRepository.GetDoctorDateAsync(request.DoctorId, date);
        var timeSlots = GetAllTimeSlots();

        // foreach (var timeSlot in timeSlots.Where(timeSlot => timeSlot.IsAvailable))
        // {
        //     if (timeSlot.IsAvailable && date.Date == DateTime.Now.Date &&
        //         timeSlot.Start <= TimeOnly.FromDateTime(DateTime.Now))
        //     {
        //         timeSlot.IsAvailable = false;
        //         continue;
        //     }
        //
        //     foreach (var appointment in appointments)
        //     {
        //         var startTime = new TimeOnly(appointment.StartDate.ToLocalTime().Hour, appointment.StartDate.Minute);
        //         var endTime =
        //             startTime.AddMinutes(appointment.Service.TimeSlotSize * TimeSlotDuration.TotalMinutes);
        //
        //         if (timeSlot.IsAvailable && timeSlot.Start >= startTime && timeSlot.End <= endTime)
        //         {
        //             timeSlot.IsAvailable = false;
        //         }
        //     }
        // }

        foreach (var appointment in appointments)
        {
            var timeSlotIndex = (appointment.StartDate.ToLocalTime().Hour - WorkingHoursStart.Hour) * 60 /
                                TimeSlotDuration.Minutes +
                                appointment.StartDate.ToLocalTime().Minute / TimeSlotDuration.Minutes;

            for (var i = 0; i < appointment.Service.TimeSlotSize; i++)
            {
                timeSlots[timeSlotIndex + i].IsAvailable = false;
            }
        }

        if (date.Date == DateTime.Now.Date)
        {
            foreach (var timeSlot in timeSlots.Where(timeSlot => timeSlot.IsAvailable))
            {
                if (timeSlot.Start <= TimeOnly.FromDateTime(DateTime.Now))
                {
                    timeSlot.IsAvailable = false;
                }
            }
        }

        return timeSlots;
    }

    private static List<TimeSlotResult> GetAllTimeSlots()
    {
        List<TimeSlotResult> timeSlots = [];
        var currentTime = WorkingHoursStart;
        while (currentTime.Add(TimeSlotDuration) <= WorkingHoursEnd)
        {
            var timeSlot = new TimeSlotResult(currentTime, currentTime.Add(TimeSlotDuration));
            timeSlots.Add(timeSlot);
            currentTime = currentTime.Add(TimeSlotDuration);
        }

        return timeSlots;
    }
}