namespace MovieClub.Infrastructure.Identity;
public class UserAccount : IdentityUser
{
    public int UserProfileId { get; set; }
    public string? DisplayName { get; set; }
}
