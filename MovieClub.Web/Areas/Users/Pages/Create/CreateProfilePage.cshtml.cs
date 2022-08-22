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
        public int UserProfileId { get; set; }

        [BindProperty]
        public CreateProfileModel PageData { get; set; } = new CreateProfileModel();

        public IActionResult OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostCreateProfile()
        {
            try
            {
                if (PageData is not null)
                {
                    if (ModelState.IsValid)
                    {
                        var currentUser = await _currentUser.GetCurrentUser(User);
                        UserProfileId = await _userCommands.CreateProfile(currentUser.Id, PageData.DisplayName!);

                        return RedirectToPage("/Home/UserhomePage", new { area = "Users" });
                    }

                    return Page();
                }

                return RedirectToPage("/NotFound");
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
