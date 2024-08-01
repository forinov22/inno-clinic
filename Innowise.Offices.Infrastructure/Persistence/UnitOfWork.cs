using Innowise.Offices.Application.Interfaces;
using Innowise.Offices.Application.Interfaces.Repositories;
using Innowise.Offices.Infrastructure.Persistence.Repositories;

namespace Innowise.Offices.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly OfficesDbContext _context;
    private readonly Lazy<IOfficeRepository> _officeRepository;

    public UnitOfWork(OfficesDbContext context)
    {
        _context = context;
        _officeRepository = new Lazy<IOfficeRepository>(() => new OfficeRepository(_context));
    }

    public IOfficeRepository OfficeRepository => _officeRepository.Value;

    public async Task SaveAllAsync()
    {
        await _context.SaveChangesAsync();
    }
}