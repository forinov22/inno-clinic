using Innowise.Offices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Innowise.Offices.Infrastructure.Persistence;

public class OfficesDbContext(DbContextOptions<OfficesDbContext> options) : DbContext(options)
{
    public DbSet<Office> Offices => Set<Office>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OfficesDbContext).Assembly);
    }
}