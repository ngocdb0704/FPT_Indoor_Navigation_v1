using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.Buildings
{
    public class listModel : PageModel
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<listModel> _logger;


        public listModel(IWebHostEnvironment webHostEnvironment, ILogger<listModel> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public async Task<IActionResult> OnPostCreateBuildingAsync()
        {

            string imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Building");

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


            return RedirectToPage("/Buildings/list");
        }

        public async Task<IActionResult> OnPostEditBuildingAsync()
        {

            string imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Building");

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


            return RedirectToPage("/Buildings/list");
        }
    }
}
