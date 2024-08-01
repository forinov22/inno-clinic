using Innowise.Offices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innowise.Offices.Infrastructure.Persistence.EntityTypeConfiguration;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.OwnsOne(office => office.Address);
    }
}