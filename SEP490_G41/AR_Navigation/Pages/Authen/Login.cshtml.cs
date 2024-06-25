using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.Authen
{
    public class LoginModel : PageModel
    {

        public void OnGet()
        {
        }

       /* public IActionResult OnPost(string username, string role)
        {
            // Lưu dữ liệu vào session
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetString("role", role);

            // Chuyển hướng đến trang chính
            return RedirectToPage("/HomePage/Home");
        }*/
    }
}
