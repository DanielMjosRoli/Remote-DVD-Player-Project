using Microsoft.EntityFrameworkCore;

public class MediaDBContext : DbContext
{
    public MediaDBContext( DbContextOptions<MediaDBContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();
}