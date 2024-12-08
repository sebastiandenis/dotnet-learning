using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController(NZWalksDbContext dbContext) : ControllerBase
    {
        private readonly NZWalksDbContext dbContext = dbContext;

        // get all regions
        // get: /api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

            var regionsDto = regions.Select(r => new RegionDto
            {
                Id = r.Id,
                Code = r.Code,
                Name = r.Name,
                RegionImageUrl = r.RegionImageUrl
            });
            return Ok(regionsDto);
        }

        // get region by id
        // get: /api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(region);
        }

        [HttpPost]
        public IActionResult Create(AddRegionRequestDto newRegion)
        {
            var region = new Region
            {
                Code = newRegion.Code,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            };
            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, UpdateRegionDto updatedRegion)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            region.Code = updatedRegion.Code;
            region.Name = updatedRegion.Name;
            region.RegionImageUrl = updatedRegion.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
