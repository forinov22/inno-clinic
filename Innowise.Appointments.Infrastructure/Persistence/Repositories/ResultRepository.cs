using Appointments.Application.Interfaces.Repositories;
using Appointments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Infrastructure.Persistence.Repositories;

internal class ResultRepository(AppointmentsDbContext context) : IResultRepository
{
    public async Task<Result?> GetByIdAsync(Guid resultId)
    {
        return await context.Results.FirstOrDefaultAsync(result => result.Id == resultId);
    }

    public async Task<Result?> GetByAppointmentIdAsync(Guid appointmentId)
    {
        return await context.Results
                            .Include(result => result.Appointment)
                            .ThenInclude(appointment => appointment.Patient)
                            .Include(result => result.Appointment)
                            .ThenInclude(appointment => appointment.Doctor)
                            .Include(result => result.Appointment)
                            .ThenInclude(appointment => appointment.Service)
                            .FirstOrDefaultAsync(result => result.AppointmentId == appointmentId);
    }

    public void Add(Result result)
    {
        context.Results.Add(result);
    }

    public void Update(Result result)
    {
        context.Results.Update(result);
    }

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}