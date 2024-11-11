using Microsoft.Extensions.Primitives;

namespace NZWalksAPI.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        
        public string Name { get; set; }

        public string? RegionImageUrl { set; get; } //nullable string type, so this can be N/A.
    }
}
