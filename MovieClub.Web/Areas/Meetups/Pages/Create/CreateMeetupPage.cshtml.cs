namespace MovieClub.Web.Areas.Meetups.Pages.Create
{
    public class CreateMeetupPage : PageModel
    {
        private readonly IAttendanceCommands _attendanceCommands;
        private readonly IMeetupCommands _meetupCommands;
        private readonly IMovieCommands _movieCommands;
        private readonly IClubService _clubPages;
        private readonly IMovieService _movieService;
        private readonly ICurrentUserService _currentUser;

        public CreateMeetupPage
        (
            IAttendanceCommands attendanceCommands,
            IMeetupCommands meetupCommands,
            IMovieCommands movieCommands,
            IClubService clubPages,
            IMovieService movieService,
            ICurrentUserService currentUser
        )
        {
            _attendanceCommands = attendanceCommands;
            _meetupCommands = meetupCommands;
            _movieCommands = movieCommands;
            _clubPages = clubPages;
            _movieService = movieService;
            _currentUser = currentUser;
        }

        [BindProperty]
        public int ClubId { get; set; }

        [BindProperty]
        public CreateMeetupModel CreateMeetupModel { get; set; } = new CreateMeetupModel();

        [BindProperty]
        public string? MovieSearchValue { get; set; }

        public List<SelectListItem> ClubSelect { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MovieSelect { get; set; } = new List<SelectListItem>();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostSearchMovies()
        {
            if (MovieSearchValue is not null)
            {
                try
                {
                    MovieSelect = await _movieService.GetSelectList(MovieSearchValue);

                    var userProfileId = await _currentUser.GetUserProfileId(HttpContext, User);
                    ClubSelect = await _clubPages.GetSelectList(userProfileId);
                }
                catch (Exception)
                {
                    return RedirectToPage("/Error");
                }                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateMeetup()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CreateMeetupModel.UserProfileId = await _currentUser.GetUserProfileId(HttpContext, User);

                    var movieId = await _movieCommands.ImportMovie(CreateMeetupModel.MovieTMDbId);
                    var meetupId = await _meetupCommands.CreateMeetup(CreateMeetupModel.UserProfileId, CreateMeetupModel.ClubId, movieId, CreateMeetupModel.Date, CreateMeetupModel.Location);
                    await _attendanceCommands.InviteClubMembers(CreateMeetupModel.UserProfileId, CreateMeetupModel.ClubId, meetupId);

                    return RedirectToPage($"/Details/MeetupDetailsPage", new { meetupId, area = "Meetups" });
                }
                catch (Exception)
                {
                    return RedirectToPage("/Error");
                }                
            }

            return Page();
        }
    }
}
