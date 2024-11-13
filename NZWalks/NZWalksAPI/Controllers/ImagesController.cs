using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        [HttpPost]
        [Route("Uload")]
        // POST: /api/Images/Upload
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                // User repository to upload image
            }

            return BadRequest(ModelState);

        }


        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };
        
            if (!allowedExtentions.Contains(Path.GetExtension(request.File.Name)))
            {
                ModelState.AddModelError("file", "Unsupported file extention, please use jpg,jpeg or png");

            }
            if(request.File.Length > 10486760) 
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file");
            }
        
        }


    }
}
