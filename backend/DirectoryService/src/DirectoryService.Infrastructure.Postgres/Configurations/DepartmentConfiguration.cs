using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentVO;
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
            .HasMaxLength(MAX_NAME_LENGTH);

        builder.Property(d => d.Name)
            .HasColumnName("name")
            .HasConversion(
                v => v.Value,
                v => DepartmentName.Create(v)
            );

        //add department path VO

        builder.Property(d => d.Slug)
            .HasColumnName("slug")
            .HasConversion(
                v => v.Value,
                v => Slug.Create(v)
            );

        builder.Property(d => d.ParentId)
            .HasColumnName("parent_id").
            HasConversion(
                v => v.Value,
                v => ParentId.Create(v)
            );
    }
}