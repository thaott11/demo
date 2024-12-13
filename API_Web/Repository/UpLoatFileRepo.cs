
namespace API_Web.Repository
{
    public class UpLoatFileRepo(IWebHostEnvironment _webHostEnvironment) : IUploatFileRepo
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".tiff", ".png" };
        private const string UploadsFolder = "FileName";

       
        public async Task<string> UpLoatFile(IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var extension = Path.GetExtension(file.FileName)?.ToLower();

            if (!_allowedExtensions.Contains(extension))
            {
                throw new Exception("Invalid file format. Only .jpg, .jpeg, and .tiff files are allowed.");
            }

            var fileName = $"{Guid.NewGuid()}{extension}";

            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, UploadsFolder);
            Directory.CreateDirectory(uploadPath); 

            var filePath = Path.Combine(uploadPath, fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/{UploadsFolder}/{fileName}";
        }


    }
}
