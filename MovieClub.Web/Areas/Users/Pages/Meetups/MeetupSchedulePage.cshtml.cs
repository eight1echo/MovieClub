namespace MovieClub.Web.Areas.Users.Pages.Meetups
{
    public class UpcomingMeetupsPage : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAttendanceCommands _attendanceCommands;

        public UpcomingMeetupsPage(IAttendanceCommands attendanceCommands, IUserService userService)
        {
            _attendanceCommands = attendanceCommands;
            _userService = userService;
        }

        [BindProperty]
        public int AttendanceId { get; set; }

        [BindProperty]
        public AttendanceStatus AttendanceStatus { get; set; }

        [BindProperty]
        public UserMeetupsModel PageData { get; set; } = null!;

        public bool filterMeetupsFuture;
        public bool filterMeetupsPast;

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                PageData = await _userService.MeetupsFuturePage(id);

                if (PageData is not null)
                {
                    filterMeetupsFuture = true;
                    return Page();
                }

                return RedirectToPage("/NotFound");
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
                PageData = await _userService.MeetupsFuturePage(id);

                if (PageData is not null)
                {
                    filterMeetupsFuture = true;
                    return Page();
                }

                return RedirectToPage("/NotFound");
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
                PageData = await _userService.MeetupsPastPage(id);

                if (PageData is not null)
                {
                    filterMeetupsPast = true;
                    return Page();
                }

                return RedirectToPage("/NotFound");
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
                return RedirectToPage("/meetups/MeetupSchedulePage", new { id = PageData.UserProfile.Id, area = "Users" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }           
        }
    }
}
