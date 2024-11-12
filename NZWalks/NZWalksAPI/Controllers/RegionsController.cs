using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

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

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(ModelState.IsValid)
            { 
            var regionsDomain = await regionRepository.GetAllAsync();

            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
            }else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            if (ModelState.IsValid)
            {
                var regionDomain = await regionRepository.GetByIdAsync(id);

                if (regionDomain == null)
                {
                    return NotFound();
                }


                return Ok(mapper.Map<RegionDto>(regionDomain));
            }else
            {
                return BadRequest();
            }
        }
        // POST to create a new region
        // POST : https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
       
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            }else
            {
                return BadRequest();
            }
        }

        // Update region
        // PUT : https://localhost:portnumber/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            if (ModelState.IsValid)
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
        
            }else
            {
                return BadRequest();
            }
        }

        // Delete region
        // Delete : https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (ModelState.IsValid) { 
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
      
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
            }else
            {
                return BadRequest();
            }

        }

    }
}
