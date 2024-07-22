using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Innowise.Appointments.RmqHost;

public class OutboxDbContext(DbContextOptions<OutboxDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
    }
}