using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MediaFileConfiguration : IEntityTypeConfiguration<MediaFile>
{
    public void Configure(EntityTypeBuilder<MediaFile> builder)
    {
        builder.ToTable("media_files");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");

        builder.Property(x => x.MovieId)
            .HasColumnName("movie_id")
            .IsRequired();

        builder.Property(x => x.StorageVolumeId)
            .HasColumnName("storage_volume_id")
            .IsRequired();

        builder.Property(x => x.FilePath)
            .HasColumnName("file_path")
            .IsRequired();

        builder.Property(x => x.FileSizeBytes)
            .HasColumnName("file_size_bytes")
            .IsRequired();

        builder.Property(x => x.Checksum)
            .HasColumnName("checksum");

        builder.Property(x => x.ContainerFormat)
            .HasColumnName("container_format");

        builder.Property(x => x.Resolution)
            .HasColumnName("resolution");

        builder.Property(x => x.AudioFormat)
            .HasColumnName("audio_format");

        builder.Property(x => x.SubtitleLanguages)
            .HasColumnName("subtitle_languages");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at");

        builder.HasOne(x => x.Movie)
            .WithMany(x => x.MediaFiles)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.StorageVolume)
            .WithMany(x => x.MediaFiles)
            .HasForeignKey(x => x.StorageVolumeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.MovieId)
            .HasDatabaseName("idx_media_movie");

        builder.HasIndex(x => x.Checksum)
            .HasDatabaseName("idx_media_checksum");

        builder.HasIndex(x => x.StorageVolumeId)
            .HasDatabaseName("idx_media_storage_volume");
    }
}