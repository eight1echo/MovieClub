namespace MovieClub.Web.Areas.Meetups.Pages.Create;

public class CreateMeetupModel
{
    [Required]
    public int ClubId { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public int MovieTMDbId { get; set; }
    public bool MovieHidden { get; set; }

    [Required]
    [MaxLength(25)]
    public string? Location { get; set; }

    [DateInTheFuture]
    public DateTime Date { get; set; } = DateTime.Now;
}