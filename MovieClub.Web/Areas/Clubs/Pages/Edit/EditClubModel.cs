namespace MovieClub.Web.Areas.Clubs.Pages.Edit;

public class EditClubModel
{
    [MaxLength(23)]
    public string? NewName { get; set; }

    public ClubDTO Club { get; set; } = new ClubDTO();
    public MembershipDTO UserMembership { get; set; } = new MembershipDTO();
}
