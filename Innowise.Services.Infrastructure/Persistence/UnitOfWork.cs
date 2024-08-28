using Innowise.Services.Application.Interfaces;
using Innowise.Services.Application.Interfaces.Repositories;
using Innowise.Services.Infrastructure.Persistence.Repositories;

namespace Innowise.Services.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ServicesDbContext _context;
    private readonly Lazy<IServiceCategoryRepository> _serviceCategoryRepository;
    private readonly Lazy<IServiceRepository> _serviceRepository;
    private readonly Lazy<ISpecializationRepository> _specializationRepository;

    public UnitOfWork(ServicesDbContext context)
    {
        _context = context;
        _serviceCategoryRepository = new Lazy<IServiceCategoryRepository>(() => new ServiceCategoryRepository(_context));
        _serviceRepository = new Lazy<IServiceRepository>(() => new ServiceRepository(_context));
        _specializationRepository = new Lazy<ISpecializationRepository>(() => new SpecializationRepository(_context));
    }
    
    public IServiceCategoryRepository ServiceCategoryRepository => _serviceCategoryRepository.Value;
    public IServiceRepository ServiceRepository => _serviceRepository.Value;
    public ISpecializationRepository SpecializationRepository => _specializationRepository.Value;
    
    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }
}