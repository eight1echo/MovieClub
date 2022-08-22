namespace MovieClub.Web.Areas.Users.Models;

public class CreateProfileModel
{
    [Required(ErrorMessage = "DisplayName is required.")]
    [MinLength(3, ErrorMessage = "DisplayName must be at least 3 characters.")]
    [MaxLength(23, ErrorMessage = "DisplayName must be less than 23 characters.")]
    public string? DisplayName { get; set; }
}
