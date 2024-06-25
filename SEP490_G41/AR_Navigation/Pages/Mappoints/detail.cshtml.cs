using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AR_Navigation.Pages.Mappoints
{
    public class detailModel : PageModel
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<listModel> _logger;


        public detailModel(IWebHostEnvironment webHostEnvironment, ILogger<listModel> logger)
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
        public async Task<IActionResult> OnPostEditMappointAsync()
        {

            string imagesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images/Mappoint");

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


            return RedirectToPage("/Mappoints/detail", new { id = Id });
        }

     
    }
}
