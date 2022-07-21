namespace MovieClub.Web.Areas.Meetups.Pages.Create;

public class CreateMeetupModel
{
    [Required]
    public int ClubId { get; set; }

    [Required]
    public int UserProfileId { get; set; }

    [Required]
    public int MovieId { get; set; }
    public bool MovieHidden { get; set; }

    [DateInTheFuture]
    public DateTime Date { get; set; }
}