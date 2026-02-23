using Microsoft.EntityFrameworkCore;

public class MediaDBContext : DbContext
{
    public MediaDBContext( DbContextOptions<MediaDBContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<MediaFile> MediaFiles => Set<MediaFile>();
    public DbSet<StorageVolume> StorageVolumes => Set<StorageVolume>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<CollectionMovie> CollectionMovies => Set<CollectionMovie>();
    public DbSet<WatchHistory> WatchHistories => Set<WatchHistory>();
    public DbSet<Rating> Ratings => Set<Rating>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaDBContext).Assembly);
    }
}