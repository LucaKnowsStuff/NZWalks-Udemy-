namespace NZWalks.API.Models.DTOs
{
    public class WalkDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthKM { get; set; }
        public string? WalkImageUrl { get; set; }
        
        public RegionDTO Region { get; set; }  
        public DifficultyDTO Difficulty { get; set; }
    }
}
