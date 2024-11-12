using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")] // adding this makes the code 3 characters long.
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { set; get; } //nullable string type, so this can be N/A.
    }
}
