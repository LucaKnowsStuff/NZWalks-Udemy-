using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository , 
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
 
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        //Endpoint to get all regions in the database
        [HttpGet]
        //[Authorize(Roles = "Reader , Writer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("Get All Regions Action Method Invocted");
                //Call the repository interface to get all regions
                var regionsDomain = await regionRepository.GetAllAsync();

                logger.LogInformation($"Finished Request with data: {JsonSerializer.Serialize(regionsDomain)}");
                //Map them to a DTO
                var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain);
                //Return Ok 200 with the region list
                return Ok(regionsDTO);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
 
        }

        //Endpoint to get a region by its id
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id)
        {   
            //Call the repository interface to get a region by its id
            var regionDomain = await regionRepository.GetByIdAsync(id);
            //Return not found if there is no such region in the database
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map the region to its DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomain);

            //Return the Ok 200 with the asked region 
            return Ok(regionDTO);
        }
        
        //Endpoit to add a region to the database
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            
            //Map the DTO given to a Region Domain Model 
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

            //Call the repository itnerface to add the region to the DB
            regionDomainModel =  await regionRepository.CreateAsync(regionDomainModel);

            //Mapp the Region Domail Model back to a DTO 
            var regionDTO = mapper.Map<RegionDTO> (regionDomainModel);
            //Return CreatedAtAction 201 and get te region created to send with the response
            return CreatedAtAction(nameof(GetbyId), new { id = regionDTO.Id} , regionDTO);
                    
        }

        //Endpoint used to update a region in the DB
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO )
        {   
            //Map the DTO back to the RDM
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

            //Update the region in the DB
            regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);
            
            //If theres no such region in the DB return Not Found 404
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map the RDM to a DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            //Return Ok 200 with the DTO
            return Ok(regionDTO);
           

        }


        //Endpoint to delete a region in the DB
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")] 
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Call the repository interface to delete the region 
            var regionDomainModel =  await regionRepository.DeleteAsync(id);
            //If theres no suck region in the DB return a NotFound 404
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Return an ok 200
            return Ok(); //We could return the delted object back , we have to use a DTO!

        }




    }

}

