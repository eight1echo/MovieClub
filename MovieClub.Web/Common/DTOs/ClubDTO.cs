namespace MovieClub.Web.Common.DTOs;
public class ClubDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<MeetupDTO> Meetups { get; set; } = new List<MeetupDTO>();
    public ICollection<MembershipDTO> Memberships { get; set; } = new List<MembershipDTO>();
}
