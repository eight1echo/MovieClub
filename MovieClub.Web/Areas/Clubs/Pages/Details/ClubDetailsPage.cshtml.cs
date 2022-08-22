namespace MovieClub.Web.Areas.Clubs.Pages.Details
{
    public class ClubDetailsPage : PageModel
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IClubService _clubService;
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IMembershipCommands _membershipCommands;

        public ClubDetailsPage(IClubService clubService, IMembershipCommands membershipCommands, ICurrentUserService currentUser, IAttendanceCommands attendanceCommands)
        {
            _clubService = clubService;
            _membershipCommands = membershipCommands;
            _currentUser = currentUser;
            _attendanceCommands = attendanceCommands;
        }

        [BindProperty]
        public int UserProfileId { get; set; }

        [BindProperty]
        public ClubMeetupsModel? Meetups { get; set; }

        [BindProperty]
        public ClubMembershipsModel? Memberships { get; set; }

        public bool _futureMeetups;
        public bool _pastMeetups;

        public async Task<IActionResult> OnGet(int clubId)
        {
            try
            {
                UserProfileId = await _currentUser.GetUserProfileId(HttpContext, User);

                Meetups = await _clubService.GetClubFutureMeetups(UserProfileId, clubId);
                Memberships = await _clubService.GetClubMemberships(UserProfileId, clubId);

                _futureMeetups = true;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnGetPastMeetups(int clubId)
        {
            try
            {
                UserProfileId = await _currentUser.GetUserProfileId(HttpContext, User);

                Meetups = await _clubService.GetClubPastMeetups(UserProfileId, clubId);
                Memberships = await _clubService.GetClubMemberships(UserProfileId, clubId);

                _pastMeetups = true;

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostAcceptMembership()
        {
            if (Memberships is not null)
            {
                await _membershipCommands.AcceptMembership(Memberships.MembershipId);
                return RedirectToPage("/Details/ClubDetailsPage", new { Memberships.ClubId, area = "Clubs" });
            }

            return RedirectToPage("/Error");
        }

        public async Task<IActionResult> OnPostRejectMembership()
        {
            if (Memberships is not null)
            {
                await _membershipCommands.DeleteMembership(Memberships.MembershipId);
                return RedirectToPage("/Details/ClubDetailsPage", new { Memberships.ClubId, area = "Clubs" });
            }

            return RedirectToPage("/Error");
        }

        public async Task<IActionResult> OnPostUpdateAttendance()
        {
            if (Meetups is not null)
            {
                await _attendanceCommands.UpdateStatus(Meetups.UserAttendance.Id, Meetups.UserAttendance.Status);
                return RedirectToPage("/Details/ClubDetailsPage", new { Meetups.Club.Id, area = "Clubs" });
            }

            return RedirectToPage("/Error");
        }
    }
}
