namespace MovieClub.Web.Areas.Meetups.Pages.Create;

public class CreateMeetupModel
{
    [Required]
    public int ClubId { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public int MovieTMDbId { get; set; }

    [Required(ErrorMessage = "A Location is required.")]
    [MaxLength(25, ErrorMessage = "Location must be less than 25 characters.")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Date and Time is required.")]
    [DateInTheFuture]
    public DateTime Date { get; set; } = DateTime.Now.Date.AddDays(1);
}