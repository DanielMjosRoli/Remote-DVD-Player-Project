using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    public void Configure(EntityTypeBuilder<MovieGenre> builder)
    {
        builder.ToTable("movie_genres");

        builder.HasKey(x => new { x.MovieId, x.GenreId });

        builder.Property(x => x.MovieId)
            .HasColumnName("movie_id");

        builder.Property(x => x.GenreId)
            .HasColumnName("genre_id");

        builder.HasOne(x => x.Movie)
            .WithMany(x => x.Genres)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Genre)
            .WithMany(x => x.Movies)
            .HasForeignKey(x => x.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}