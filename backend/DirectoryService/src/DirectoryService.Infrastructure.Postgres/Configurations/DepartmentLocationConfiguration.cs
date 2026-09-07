using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_location");

        builder.HasKey(dl => dl.Id).HasName("pk_department_location");
        builder.Property(dl => dl.Id).HasColumnName("id");

        // 1. Связь с родителем (Department) через теневой внешний ключ
        builder.HasOne(dl => dl.Department)
            .WithMany(d => d.Locations)
            .HasForeignKey("department_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // 2. Внешний ключ на Location без навигационного свойства объекта
        builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(dl => dl.LocationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(dl => dl.LocationId).HasColumnName("location_id");
    }
}