using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddWalkRequestDto
    {
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description has to be a maximum of 200 characters")]
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; } //this is a nullable variable as well

        public Guid DifficultyId { get; set; }

        public Guid RegionId { get; set; }
    }
}
