namespace MovieClub.Web.Areas.Users.Pages.Create;

public class CreateProfileModel
{
    [Required]
    [MaxLength(23)]
    public string? DisplayName { get; set; }
}
