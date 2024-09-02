using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Profiles.Application.Interfaces;
using Profiles.Application.Interfaces.Repositories;
using Profiles.Infrastructure.Persistence.Repositories;

namespace Profiles.Infrastructure.Persistence;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ProfilesDbContext _context;
    private readonly Lazy<AccountRepository> _accountRepository;
    private readonly Lazy<DoctorRepository> _doctorRepository;
    private readonly Lazy<OfficeRepository> _officeRepository;
    private readonly Lazy<PatientRepository> _patientRepository;
    private readonly Lazy<ReceptionistRepository> _receptionistRepository;
    private readonly Lazy<SpecializationRepository> _specializationRepository;

    public UnitOfWork(
        ProfilesDbContext context,
        HttpClient httpClient,
        IDistributedCache distributedCache,
        IConfiguration configuration,
        ILogger<OfficeRepository> logger1,
        ILogger<SpecializationRepository> logger2)
    {
        _context = context;
        _accountRepository = new Lazy<AccountRepository>(() => new AccountRepository(_context));
        _doctorRepository = new Lazy<DoctorRepository>(() => new DoctorRepository(_context));
        _officeRepository = new Lazy<OfficeRepository>(() => new OfficeRepository(httpClient, distributedCache, configuration, logger1));
        _patientRepository = new Lazy<PatientRepository>(() => new PatientRepository(_context));
        _receptionistRepository = new Lazy<ReceptionistRepository>(() => new ReceptionistRepository(_context));
        _specializationRepository = new Lazy<SpecializationRepository>(() => new SpecializationRepository(httpClient, distributedCache, configuration, logger2));
    }
    
    public IAccountRepository AccountRepository => _accountRepository.Value;
    public IDoctorRepository DoctorRepository => _doctorRepository.Value;
    public IOfficeRepository OfficeRepository => _officeRepository.Value;
    public IPatientRepository PatientRepository => _patientRepository.Value;
    public IReceptionistRepository ReceptionistRepository => _receptionistRepository.Value;
    public ISpecializationRepository SpecializationRepository => _specializationRepository.Value;
    
    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }
}