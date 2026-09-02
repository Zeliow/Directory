using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;
using DirectoryService.Domain.LocationVO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    private const int MAX_NAME_LENGTH = 100;

    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        builder.HasKey(d => d.Id)
            .HasName("id");

        builder.Property(d => d.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(MAX_NAME_LENGTH)
            .HasConversion(
                v => v.Value,
                v => DepartmentName.Create(v)
            );

        builder.Property(d => d.Path)
            .HasColumnName("path")
            .IsRequired(false)
            .HasConversion(
                v => v == null ? null : v.Value,
                v => v == null ? null : DepartmentPath.Create(v));

        builder.Property(d => d.Slug)
            .HasColumnName("slug")
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => Slug.Create(v)
            );

        builder.Property(d => d.ParentId)
            .HasColumnName("parent_id")
            .IsRequired(false)
            .HasConversion(
                v => v == null ? null : v.Value,
                v => v == null ? null : ParentId.Create(v)
            );
    }
}