using Microsoft.EntityFrameworkCore;
using Services.Domain;

namespace Innowise.Services.Infrastructure.Persistence;

public class ServicesDbContext(DbContextOptions<ServicesDbContext> options) : DbContext(options)
{
    public DbSet<Service> Services => Set<Service>();
    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();
    public DbSet<Specialization> Specializations => Set<Specialization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServicesDbContext).Assembly);
    }
}