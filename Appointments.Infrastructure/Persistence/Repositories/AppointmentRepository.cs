using Appointments.Application.Interfaces.Repositories;
using Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure.Persistence.Repositories;

internal class AppointmentRepository(AppointmentsDbContext context) : IAppointmentRepository
{
    public async Task<IEnumerable<Appointment>> GetAllUpcomingAsync()
    {
        return await context.Appointments
                            .Where(appointment => appointment.StartDate >= DateTime.UtcNow)
                            .Include(appointment => appointment.Patient)
                            .Include(appointment => appointment.Doctor)
                            //.Include(appointment => appointment.Service)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetDoctorUpcomingAsync(Guid doctorId)
    {
        return await context.Appointments
                            .Where(appointment =>
                                       appointment.DoctorId == doctorId && appointment.StartDate >= DateTime.UtcNow &&
                                       appointment.IsApproved)
                            .Include(appointment => appointment.Patient)
                            .Include(appointment => appointment.Doctor)
                            //.Include(appointment => appointment.Service)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetDoctorDateAsync(Guid doctorId, DateTime date)
    {
        return await context.Appointments
                            .Where(appointment =>
                                       appointment.DoctorId == doctorId &&
                                       appointment.StartDate.ToLocalTime().Date == date.Date && appointment.IsApproved)
                            .Include(appointment => appointment.Patient)
                            .Include(appointment => appointment.Doctor)
                            //.Include(appointment => appointment.Service)
                            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetPatientHistoryAsync(Guid patientId)
    {
        return await context.Appointments
                            .Where(appointment => appointment.PatientId == patientId && appointment.IsApproved)
                            .Include(appointment => appointment.Patient)
                            .Include(appointment => appointment.Doctor)
                            //.Include(appointment => appointment.Service)
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetAllStartingTomorrowAsync()
    {
        return await context.Appointments
                            .Include(appointment => appointment.Patient)
                            .Include(appointment => appointment.Doctor)
                            .Where(appointment =>
                                       appointment.StartDate.ToLocalTime().Date == DateTime.Now.Date.AddDays(1))
                            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(Guid appointmentId)
    {
        return await context.Appointments
                            .Include(appointment => appointment.Patient)
                            .Include(appointment => appointment.Doctor)
                            //.Include(appointment => appointment.Service)
                            .FirstOrDefaultAsync(appointment => appointment.Id == appointmentId);
    }

    public void Add(Appointment appointment)
    {
        context.Appointments.Add(appointment);
    }

    public void Remove(Appointment appointment)
    {
        context.Appointments.Remove(appointment);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}