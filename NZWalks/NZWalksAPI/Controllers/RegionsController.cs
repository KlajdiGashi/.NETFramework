using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace NZWalksAPI.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,IMapper mapper,ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [ValidateModel]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("GetAllRegions Action Method was invoked");

                logger.LogWarning("This is a warning log!");

                logger.LogError("This is an error log!");

                var regionsDomain = await regionRepository.GetAllAsync();

                var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

                logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                return Ok(regionsDto);
            }
             catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
                var regionDomain = await regionRepository.GetByIdAsync(id);

                if (regionDomain == null)
                {
                    return NotFound();
                }


                return Ok(mapper.Map<RegionDto>(regionDomain));

        }
        // POST to create a new region
        // POST : https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
       
           
        }

        // Update region
        // PUT : https://localhost:portnumber/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                await dbContext.SaveChangesAsync();

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return Ok(mapper.Map<RegionDto>(regionDomainModel));
        
           
        }

        // Delete region
        // Delete : https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {   
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
      
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }

    }
}
