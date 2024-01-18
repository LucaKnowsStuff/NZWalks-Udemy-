using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Models.DTOs
{
    public class ImageUploadRequestDTO
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        
    }
}
