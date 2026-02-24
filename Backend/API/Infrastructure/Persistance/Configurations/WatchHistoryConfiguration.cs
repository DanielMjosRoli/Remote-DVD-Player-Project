using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WatchHistoryConfiguration : IEntityTypeConfiguration<WatchHistory>
{
    public void Configure(EntityTypeBuilder<WatchHistory> builder)
    {
        builder.ToTable("watch_history");

        builder.HasKey(x => new { x.UserId, x.MovieId });

        builder.Property(x => x.UserId)
            .HasColumnName("user_id");

        builder.Property(x => x.MovieId)
            .HasColumnName("movie_id");

        builder.Property(x => x.LastPositionSeconds)
            .HasColumnName("last_position_seconds");

        builder.Property(x => x.Completed)
            .HasColumnName("completed");

        builder.Property(x => x.LastWatchedAt)
            .HasColumnName("last_watched_at");

        builder.HasOne(x => x.User)
            .WithMany(x => x.WatchHistory)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Movie)
            .WithMany()
            .HasForeignKey(x => x.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("idx_watchhistory_user");
    }
}