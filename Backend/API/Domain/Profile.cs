public class Profile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string? Avatar { get; set; }
    public bool IsKids { get; set; }

    public User User { get; set; }
}