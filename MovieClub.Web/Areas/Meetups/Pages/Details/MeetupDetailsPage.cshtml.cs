namespace MovieClub.Web.Areas.Meetups.Pages.Details
{
    public class MeetupDetailsPage : PageModel
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IMeetupService _meetupService;

        public MeetupDetailsPage(IMeetupService meetupService, ICurrentUserService currentUser, IAttendanceCommands attendanceCommands)
        {
            _meetupService = meetupService;
            _currentUser = currentUser;
            _attendanceCommands = attendanceCommands;
        }

        [BindProperty]
        public int UserProfileId { get; set; }

        [BindProperty]
        public MeetupDetailsModel? MeetupDetails { get; set; }

        public async Task<IActionResult> OnGet(int meetupId)
        {
            UserProfileId = await _currentUser.GetUserProfileId(HttpContext, User);
            MeetupDetails = await _meetupService.GetMeetupDetails(UserProfileId, meetupId);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAttendance()
        {
            try
            {
                if (MeetupDetails is not null)
                {
                    await _attendanceCommands.UpdateStatus(MeetupDetails.Meetup.UserAttendance.Id, MeetupDetails.Meetup.UserAttendance.Status);

                    return RedirectToPage("/Details/MeetupDetailsPage", new { MeetupDetails.Meetup.Id, area = "Meetups" });
                }

                return RedirectToPage("/Error");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
