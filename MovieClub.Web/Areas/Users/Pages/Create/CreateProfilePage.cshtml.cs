namespace MovieClub.Web.Areas.Users.Pages.Create
{
    public class CreateProfilePage : PageModel
    {
        private readonly IUserCommands _userCommands;
        private readonly ICurrentUserService _currentUser;

        public CreateProfilePage(ICurrentUserService currentUser, IUserCommands userCommands)
        {
            _currentUser = currentUser;
            _userCommands = userCommands;
        }

        [BindProperty]
        public CreateProfileModel PageData { get; set; }
        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostCreateProfile()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = await _currentUser.GetCurrentUser(User);
                    var profileId = await _userCommands.CreateProfile(currentUser.Id, PageData.DisplayName!);

                    return RedirectToPage("/profile/UserProfilePage", new { id = profileId, area = "Users" });
                }

                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
