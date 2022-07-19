namespace MovieClub.Web.Areas.Clubs.Pages.Index;
public class ClubIndexModel
{
    public List<ClubDTO> ClubsLeader { get; set; } = new List<ClubDTO>();
    public List<ClubDTO> ClubsMember { get; set; } = new List<ClubDTO>();
    public List<ClubDTO> ClubsPending { get; set; } = new List<ClubDTO>();
}
