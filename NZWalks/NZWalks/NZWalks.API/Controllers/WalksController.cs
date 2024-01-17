using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper , IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn , [FromQuery] string? filterQuery , 
            [FromQuery] string? sortby , [FromQuery] bool order,
            [FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 1000) 
        {
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn , filterQuery , sortby , order , pageNumber , pageSize);
            return Ok(mapper.Map<List<WalkDTO>>(walkDomainModel));
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {   
            
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);
            await walkRepository.CreateAsync(walkDomainModel);
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
           
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateWalkRequestDTO updateWalkDTO)
        {
            
            var walkDomainModel = mapper.Map<Walk>(updateWalkDTO);

            walkDomainModel = await walkRepository.Update(id , walkDomainModel);

            if (walkDomainModel == null )
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
           
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteByIdAsync(id);
            if (walkDomainModel == null )
            {
                return NotFound();
            }

            return Ok();
        }

    }

   
}