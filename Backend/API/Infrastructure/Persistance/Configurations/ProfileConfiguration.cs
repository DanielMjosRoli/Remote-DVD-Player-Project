using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> entity)
    {
        entity.ToTable("profiles");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasColumnName("name");

        entity.Property(e => e.Avatar)
            .HasColumnName("avatar");

        entity.Property(e => e.IsKids)
            .HasColumnName("is_kids");

        entity.Property(e => e.UserId)
            .HasColumnName("user_id");

        entity.HasOne(e => e.User)
            .WithMany(u => u.Profiles)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}