namespace MovieClub.Web.Areas.Clubs.Pages.Create;

public class CreateClubModel
{
    [Required]
    [MaxLength(25)]
    public string? Name { get; set; }
}
