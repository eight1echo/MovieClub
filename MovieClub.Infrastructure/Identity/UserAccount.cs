namespace MovieClub.Infrastructure.Identity;
public class UserAccount : IdentityUser
{
    public int UserProfileId { get; private set; }

    public void SetProfileId(int profileId)
    {
        UserProfileId = profileId;
    }
}
