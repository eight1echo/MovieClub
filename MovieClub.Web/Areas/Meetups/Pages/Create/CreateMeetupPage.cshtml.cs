namespace MovieClub.Web.Areas.Meetups.Pages.Create
{
    public class CreateMeetupPage : PageModel
    {
        private readonly IAttendanceCommandService _attendanceCommands;
        private readonly IClubQueryService _clubQueries;
        private readonly IMeetupCommandService _meetupCommands;
        private readonly IMovieCommandService _movieCommands;
        private readonly IMovieQueryService _movieQueries;
        private readonly ICurrentUserService _currentUser;

        public CreateMeetupPage(IClubQueryService clubQueries,
            ICurrentUserService currentUser,
            IMeetupCommandService meetupCommands,
            IMovieQueryService movieQueries,
            IMovieCommandService movieCommands,
            IAttendanceCommandService attendanceCommands)
        {
            _attendanceCommands = attendanceCommands;
            _clubQueries = clubQueries;
            _currentUser = currentUser;
            _meetupCommands = meetupCommands;
            _movieQueries = movieQueries;
            _movieCommands = movieCommands;
        }

        [BindProperty]
        public int ClubId { get; set; }

        [BindProperty]
        public CreateMeetupModel CreateMeetupModel { get; set; } = new CreateMeetupModel();

        [BindProperty]
        public string? MovieSearchValue { get; set; }
        public List<SelectListItem> MovieSelect { get; set; } = new List<SelectListItem>();

        public IActionResult OnGet(int id)
        {  
            ClubId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostSearchMovies()
        {
            if (MovieSearchValue is not null)
                MovieSelect = await _movieQueries.MovieSelectQuery(MovieSearchValue);

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (ModelState.IsValid)
            {
                CreateMeetupModel.UserProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

                var movieId = await _movieCommands.ImportMovie(CreateMeetupModel.MovieTMDbId);
                var meetupId = await _meetupCommands.CreateMeetup(CreateMeetupModel, movieId);
                await _attendanceCommands.InviteClubMembers(CreateMeetupModel.UserProfileId, CreateMeetupModel.ClubId, meetupId);

                return RedirectToPage("/home", new { area = "Home" });
            }

            return Page();
        }
    }
}
