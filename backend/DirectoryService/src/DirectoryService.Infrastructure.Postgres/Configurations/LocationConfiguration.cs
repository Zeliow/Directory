using DirectoryService.Domain;
using DirectoryService.Domain.LocationVO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    private const int MAX_NAME_LENGTH = 100;
    private const int MAX_ADDRESS_LENGTH = 200;

    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");
        builder.HasKey(l => l.Id)
            .HasName("id");

        builder.Property(l => l.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(MAX_NAME_LENGTH);

        builder.Property(l => l.Name)
            .HasColumnName("name")
            .HasConversion(
                name => name.Value,
                value => LocationName.Create(value));

        builder.Property(l => l.Address)
            .HasColumnName("address")
            .IsRequired()
            .HasMaxLength(MAX_ADDRESS_LENGTH);

        builder.Property(l => l.Address)
            .HasColumnName("address")
            .HasConversion(
                address => address.Value,
                value => Address.Create(value));
    }
}