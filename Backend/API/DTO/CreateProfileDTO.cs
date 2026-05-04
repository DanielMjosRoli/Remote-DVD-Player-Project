public class CreateProfileDto
{
    public string Name { get; set; } = null!;
    public string? Avatar { get; set; }
    public bool IsKids { get; set; }
    public Guid UserId { get; set; }
}