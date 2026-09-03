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
            .HasMaxLength(MAX_NAME_LENGTH)
            //туда в БД и обратно из БД
            .HasConversion(
                name => name.Value,
                value => LocationName.Create(value));

        // Configure the Address property with a value object conversion
        // Use Complex Property Mapping to map the Address value object to a single column in the database
        builder.Property(l => l.Address)
            .HasColumnName("address")
            .IsRequired()
            .HasMaxLength(MAX_ADDRESS_LENGTH)
            .HasConversion(
                address => address.Value,
                value => Address.CreateVO(value));
    }
}