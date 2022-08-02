namespace MovieClub.Web.Areas.Clubs.Pages.Meetups
{
    public class MeetupSchedulePage : PageModel
    {
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IClubService _clubPages;
        private readonly ICurrentUserService _currentUser;

        public MeetupSchedulePage(IClubService clubPages, ICurrentUserService currentUser, IAttendanceCommands attendanceCommands)
        {
            _clubPages = clubPages;
            _currentUser = currentUser;
            _attendanceCommands = attendanceCommands;
        }

        [BindProperty]
        public ClubMeetupsModel PageData { get; set; }

        [BindProperty]
        public int AttendanceId { get; set; }

        [BindProperty]
        public AttendanceStatus AttendanceStatus { get; set; }

        public bool filterMeetupsFuture;
        public bool filterMeetupsPast;

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                int userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _clubPages.MeetupsFuturePage(userProfileId, id);

                filterMeetupsFuture = true;
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostShowFutureMeetups(int id)
        {
            try
            {
                int userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _clubPages.MeetupsFuturePage(userProfileId, id);

                filterMeetupsFuture = true;
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostShowPastMeetups(int id)
        {
            try
            {
                int userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _clubPages.MeetupsPastPage(userProfileId, id);

                filterMeetupsPast = true;
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostUpdateAttendance()
        {
            try
            {
                await _attendanceCommands.UpdateStatus(AttendanceId, AttendanceStatus);
                return RedirectToPage("/meetups/MeetupSchedulePage", new { id = PageData.Club.Id, area = "Clubs" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
