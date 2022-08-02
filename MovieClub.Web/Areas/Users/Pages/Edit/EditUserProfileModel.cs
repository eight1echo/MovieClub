namespace MovieClub.Web.Areas.Users.Pages.Edit;

public class EditUserProfileModel
{
    public string? NewDisplayName { get; set; }
    public UserProfileDTO UserProfile { get; set; } = new UserProfileDTO();
}
