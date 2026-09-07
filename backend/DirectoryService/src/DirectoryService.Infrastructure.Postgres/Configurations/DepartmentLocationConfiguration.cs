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

        builder.HasOne(dl => dl.Department)
            .WithMany(d => d.Locations)
            .HasForeignKey(dl => dl.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_department_location_department");
    }
}