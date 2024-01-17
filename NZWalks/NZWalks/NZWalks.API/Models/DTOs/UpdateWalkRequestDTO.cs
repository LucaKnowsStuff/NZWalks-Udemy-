using System.ComponentModel.DataAnnotations;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTOs
{
    public class UpdateWalkRequestDTO
    {

        [Required]
        [MaxLength(45, ErrorMessage = "Walk name can´t have more than 45 characters!")]
        public string Name { get; set; }
        [Required]
        [MinLength(15, ErrorMessage = "Walk description can´t have less than 15 characters!")]
        [MaxLength(145, ErrorMessage = "Walk description can´t have more than 145 characters!")]
        public string Description { get; set; }
        [Required]
        [Range(0, 95, ErrorMessage = "Walk can´t have more than 95 Km!")]
        public double LengthKM { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }

    }

}
