using NZWalksAPI.Data;
using NZWalksAPI.Migrations;
using NZWalksAPI.Models.Domain;
using System.Net;

namespace NZWalksAPI.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            
            
            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            // stream reads the file stream and creates the file.
            await image.File.CopyToAsync(stream);
            //passing the stream to the copy to async


            // https://localhost:1234/images/image.jpg

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Add Image to the Images table 
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        
        
        }
    }
}
