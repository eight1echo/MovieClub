namespace MovieClub.Web.Areas.Users.Pages.Edit
{
    public class EditUserProfilePage : PageModel
    {
        public readonly IUserService _userPages;
        private readonly IUserCommands _userCommands;
        private readonly ICurrentUserService _currentUser;

        public EditUserProfilePage(ICurrentUserService currentUser, IUserService userPages, IUserCommands userCommands)
        {
            _currentUser = currentUser;
            _userPages = userPages;
            _userCommands = userCommands;
        }

        [BindProperty]
        public EditUserProfileModel? PageData { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                PageData = await _userPages.EditPage(id);

                if (PageData is null)
                    return RedirectToPage("/NotFound");

                if (PageData.UserProfile.Id != userProfileId)
                    return RedirectToPage("/AccessDenied");
                    
                return Page();
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostDeleteProfile()
        {
            try
            {
                if (PageData is not null)
                {
                    var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);

                    if (PageData.UserProfile.Id == userProfileId)
                    {
                        await _userCommands.DeleteProfile(PageData.UserProfile.Id);
                        await _currentUser.ClearSessionState(HttpContext);
                        return RedirectToPage("/Create/CreateProfilePage", new { area = "Users" });
                    }
                        
                    return RedirectToPage("/AccessDenied");                    
                }

                return RedirectToPage("/NotFound");             
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostUpdateProfile()
        {
            try
            {
                if (PageData is null)
                    return RedirectToPage("/NotFound");

                var userProfileId = await _currentUser.GetProfileIdFromSession(HttpContext, User);
                if (PageData.UserProfile.Id != userProfileId)
                    return RedirectToPage("/AccessDenied");

                await _userCommands.UpdateProfile(PageData.UserProfile.Id, PageData.NewDisplayName);
                return RedirectToPage("/Profile/UserProfilePage", new { id = PageData.UserProfile.Id, area = "Users" });
            }
            catch (Exception)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
