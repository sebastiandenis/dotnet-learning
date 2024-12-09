using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository) : ControllerBase
    {
        private readonly NZWalksDbContext dbContext = dbContext;
        private readonly IRegionRepository regionRepository = regionRepository;

        // get all regions
        // get: /api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);
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
        public async Task<IActionResult> Create(AddRegionRequestDto newRegion)
        {
            var region = await regionRepository.CreateAsync(new Region
            {
                Code = newRegion.Code,
                Name = newRegion.Name,
                RegionImageUrl = newRegion.RegionImageUrl
            });

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
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRegionDto updatedRegion)
        {
            var region = new Region
            {
                Code = updatedRegion.Code,
                Name = updatedRegion.Name,
                RegionImageUrl = updatedRegion.RegionImageUrl
            };

            region = await regionRepository.UpdateAsync(id, region);

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

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
