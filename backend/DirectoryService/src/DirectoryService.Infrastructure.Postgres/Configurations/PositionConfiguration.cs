using DirectoryService.Domain;
using DirectoryService.Domain.PositionVO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    private const int MAX_NAME_LENGTH = 100;

    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");
        builder.HasKey(p => p.Id)
            .HasName("pk_positions");

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasMaxLength(MAX_NAME_LENGTH)
            .IsRequired()
            .HasConversion(
                name => name.Value,
                value => PositionName.Create(value));

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
    }
}