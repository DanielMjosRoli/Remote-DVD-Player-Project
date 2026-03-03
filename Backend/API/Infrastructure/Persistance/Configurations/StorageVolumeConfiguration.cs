using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StorageVolumeConfiguration : IEntityTypeConfiguration<StorageVolume>
{
    public void Configure(EntityTypeBuilder<StorageVolume> builder)
    {
        builder.ToTable("storage_volumes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.MountPath)
            .HasColumnName("mount_path")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(x => x.Type)
            .HasColumnName("type")
            .IsRequired();

        builder.Property(x => x.CapacityBytes)
            .HasColumnName("capacity_bytes");

        builder.Property(x => x.AvailableBytes)
            .HasColumnName("available_bytes");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");
    }
}