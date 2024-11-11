using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mapping
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap/Mapping needs a source and a destination
            // CreateMap<>

            CreateMap<Region, RegionDto>().ReverseMap(); // reverse map if we need reverse mapping, so from region dto to region

            CreateMap<AddRegionRequestDto, Region>().ReverseMap(); // public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }

    }
}
