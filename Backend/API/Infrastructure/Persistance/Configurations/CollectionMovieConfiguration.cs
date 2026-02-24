using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CollectionMovieConfiguration : IEntityTypeConfiguration<CollectionMovie>
{
    public void Configure(EntityTypeBuilder<CollectionMovie> builder)
    {
        builder.ToTable("collection_movies");

        builder.HasKey(x => new { x.CollectionId, x.MovieId });

        builder.Property(x => x.CollectionId)
            .HasColumnName("collection_id");

        builder.Property(x => x.MovieId)
            .HasColumnName("movie_id");

        builder.HasOne(x => x.Collection)
            .WithMany(x => x.Movies)
            .HasForeignKey(x => x.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Movie)
            .WithMany(x => x.Collections)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}