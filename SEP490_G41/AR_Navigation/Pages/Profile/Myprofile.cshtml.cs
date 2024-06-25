using AR_Navigation.Pages.Buildings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.Profile
{
    public class MyprofileModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<MyprofileModel> _logger;
        public MyprofileModel(IWebHostEnvironment webHostEnvironment, ILogger<MyprofileModel> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public async Task<IActionResult> OnPostEditProfileAsync()
        {

            string imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Avatar");

            string uniqueFileName = null;
            if (ImageFile != null && ImageFile.Length > 0)
            {
                uniqueFileName = ImageFile.FileName;

                string filePath = Path.Combine(imagesDirectory, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
            }
            _logger.LogInformation($"Images directory: {imagesDirectory}");


            return RedirectToPage("/Profile/Myprofile");
        }
    }
}
