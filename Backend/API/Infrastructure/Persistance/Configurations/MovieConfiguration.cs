using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("movies");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .IsRequired();

        builder.Property(x => x.OriginalTitle)
            .HasColumnName("original_title");

        builder.Property(x => x.Description)
            .HasColumnName("description");

        builder.Property(x => x.ReleaseYear)
            .HasColumnName("release_year");

        builder.Property(x => x.DurationMinutes)
            .HasColumnName("duration_minutes");

        builder.Property(x => x.AgeRating)
            .HasColumnName("age_rating");

        builder.Property(x => x.PosterPath)
            .HasColumnName("poster_path");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(x => x.Title)
            .HasDatabaseName("idx_movies_title");
    }
}