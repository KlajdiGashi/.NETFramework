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

            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
            /* 
             so basically Mapper is saying to automatically connect the variables with the same names on the DTO and the Region Domain where then the data is sent to the Server/Database.
            If we say Region,RegionDto. basically were saying connect the same names with the same variables, just as we did earlier

            var RegionDto = new Region 
            {
            Name = RegionDto.Name,
            Code = RegionDto.Code,
            ImageUrl = RegionDto.ImageUrl
            };

            and then calling this new Manual mapping that we have created to get the data we needed, because as we said to get that data we first need to go to the DTO(Data transfer object), and then that DTO will access the domain model and then that domain model will access the Database with the NZWalksDbContext, which is an input in the RegionsController, including the mapper and the regionsRepository.
            */
        }

    }
}
