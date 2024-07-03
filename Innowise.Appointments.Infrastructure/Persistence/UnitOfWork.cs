using Appointments.Application.Interfaces;
using Appointments.Application.Interfaces.Repositories;
using Appointments.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Appointments.Infrastructure.Persistence;

public class UnitOfWork(
    AppointmentsDbContext context,
    HttpClient httpClient,
    IDistributedCache distributedCache,
    IConfiguration configuration,
    ILogger<ServiceRepository> logger)
    : IUnitOfWork
{
    private readonly Lazy<IAppointmentRepository>
        _appointmentRepository = new(() => new AppointmentRepository(context));

    private readonly Lazy<IDoctorRepository> _doctorRepository = new(() => new DoctorRepository(context));
    private readonly Lazy<IPatientRepository> _patientRepository = new(() => new PatientRepository(context));
    private readonly Lazy<IResultRepository> _resultRepository = new(() => new ResultRepository(context));

    private readonly Lazy<IServiceRepository> _serviceRepository =
        new(() => new ServiceRepository(httpClient, distributedCache, configuration, logger));


    public IAppointmentRepository AppointmentRepository => _appointmentRepository.Value;
    public IDoctorRepository DoctorRepository => _doctorRepository.Value;
    public IPatientRepository PatientRepository => _patientRepository.Value;
    public IResultRepository ResultRepository => _resultRepository.Value;
    public IServiceRepository ServiceRepository => _serviceRepository.Value;

    public async Task SaveAllAsync()
    {
        await context.SaveChangesAsync();
    }
}