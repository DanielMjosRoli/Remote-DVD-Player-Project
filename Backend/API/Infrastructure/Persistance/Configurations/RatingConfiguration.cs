using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("ratings");

        builder.HasKey(x => new { x.UserId, x.MovieId });

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");

        builder.Property(x => x.MovieId)
            .HasColumnName("movie_id");

        builder.Property(x => x.RatingValue)
            .HasColumnName("rating_value")
            .IsRequired();

        builder.Property(x => x.RatedAt)
            .HasColumnName("rated_at");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Ratings)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Movie)
            .WithMany(x => x.Ratings)
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.MovieId)
            .HasDatabaseName("idx_ratings_movie");
    }
}