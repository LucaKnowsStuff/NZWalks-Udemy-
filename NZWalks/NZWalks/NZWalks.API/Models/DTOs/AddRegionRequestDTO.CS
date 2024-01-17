using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3 , ErrorMessage = "Region code needs to have exacly 3 characters!")]
        [MaxLength(3 , ErrorMessage = "Region code needs to have exacly 3 characters!")]
        public string Code { get; set; } 
        [Required]
        [MaxLength(45 , ErrorMessage = "Region name can´t have more than 45 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
