using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    //api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;
        public RegionsController(NZWalksDBContext dBContext)
        {
            this.dbContext = dBContext;
        }



        [HttpGet]
        public IActionResult GetAll()
        {

            var regions = dbContext.Regions.ToList();
            
            var regionsDTO = new List<RegionDTO>();
            foreach (var region in regions) 
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                }
                    );
            }
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetbyId([FromRoute] Guid id)
        {   

            var region = dbContext.Regions.Find(id);

            

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };


            return Ok(regionDTO);
        }
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetbyId), new { id = regionDTO.Id} , regionDTO);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO )
        {
            var regionDomainModule = dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (regionDomainModule == null)
            {
                return NotFound();
            }

            regionDomainModule.Code = updateRegionRequestDTO.Code;
            regionDomainModule.Name = updateRegionRequestDTO.Name;  
            regionDomainModule.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            dbContext.SaveChanges();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModule.Id,
                Code = regionDomainModule.Code,
                Name = regionDomainModule.Name,
                RegionImageUrl = regionDomainModule.RegionImageUrl
            };

            return Ok(regionDTO);


        }
    }

}

