using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository )
        {
            this.imageRepository = imageRepository;
        }




        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };


                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel); //? Shoudn´t it be the DTO?
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file" , "Unsupproted file extension!");
            }

            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("files" , "File size cannot be bigger than 10 MB!");
            }
        }
    }
}
