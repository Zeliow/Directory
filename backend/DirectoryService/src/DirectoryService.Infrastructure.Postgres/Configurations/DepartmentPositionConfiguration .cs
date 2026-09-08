using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_position");

        builder.HasKey(dp => dp.Id).HasName("pk_department_position");
        builder.Property(dp => dp.Id).HasColumnName("id");

        // 1. Связь с родителем (Department) через теневой внешний ключ
        builder.HasOne(dp => dp.Department)
            .WithMany(d => d.Positions)
            .HasForeignKey("department_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // 2. Внешний ключ на Position без навигационного свойства объекта
        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(dp => dp.PositionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(dp => dp.PositionId).HasColumnName("position_id");
    }
}