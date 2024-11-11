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

    }
}
