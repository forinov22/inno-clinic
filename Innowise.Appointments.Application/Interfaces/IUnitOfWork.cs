using Appointments.Application.Interfaces.HttpClients;
using Appointments.Application.Interfaces.HttpClients.Services;
using Appointments.Application.Interfaces.Repositories;

namespace Appointments.Application.Interfaces;

public interface IUnitOfWork
{
    IAppointmentRepository AppointmentRepository { get; }
    IDoctorRepository DoctorRepository { get; }
    IPatientRepository PatientRepository { get; }
    IResultRepository ResultRepository { get; }
    // IServiceHttpClient ServiceRepository { get; }
    Task SaveAllAsync();
}