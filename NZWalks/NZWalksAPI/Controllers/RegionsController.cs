using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Controllers
{
    // https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var regions = dbContext.Regions.ToList();

            dbContext.Regions.ToList();

           /* var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl =""
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl =""
                }
            };

            */

            return Ok(regions);
        }
        // Get single region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) 
        {
            // var region = dbContext.Regions.Find(id); // find can only be used with ID

            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id); // getting the x so that the Id we pass to match the Id in the database

            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }

    }
}
