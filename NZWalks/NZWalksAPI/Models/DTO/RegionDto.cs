namespace NZWalksAPI.Models.DTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { set; get; } //nullable string type, so this can be N/A.
    }
}
