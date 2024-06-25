using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.HomePage
{
    public class HomeModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
       /* public IActionResult OnGet()
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("username");
            var role = _httpContextAccessor.HttpContext.Session.GetInt32("role");

            if (!string.IsNullOrEmpty(username) && role.HasValue)
            {
                return RedirectToPage("/Authen/Login");
            }

            return Page();
        }*/
    }
}
