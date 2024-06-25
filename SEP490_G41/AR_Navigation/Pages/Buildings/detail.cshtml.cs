using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.Buildings
{
    public class DetailModel : PageModel
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<listModel> _logger;


        public DetailModel(IWebHostEnvironment webHostEnvironment, ILogger<listModel> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }


        public void OnGet([FromRoute] int id)
        {
            Id = id;
        }
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public async Task<IActionResult> OnPostAddOrEditMapAsync()
        {

            string imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Map");

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


            return RedirectToPage("/Buildings/Detail", new { id = Id });
        }

        public async Task<IActionResult> OnPostEditMappointMapAsync()
        {

            string imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Map");

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

            return RedirectToPage("/Buildings/Detail", new { id = Id });

        }
    }
}
