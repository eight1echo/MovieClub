namespace MovieClub.Web.Areas.Clubs.Models;

public class CreateClubModel
{
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
    [MaxLength(30, ErrorMessage = "Name must be less than 30 characters.")]
    public string Name { get; set; } = string.Empty;
}
