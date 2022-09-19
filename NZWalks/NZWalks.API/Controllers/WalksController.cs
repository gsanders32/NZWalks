using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //Fetch data from database - domain walks
            var walksDomain = await _walkRepository.GetAllAsync();

            //Convert domain walk to DTO walks
            var walksDTO = _mapper.Map<List<Models.DTO.Walk>>(walksDomain);

            //Return response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //Get Walk Domain object fron database
            var walkDomain = await _walkRepository.GetAsync(id);

            //Check if found
            if (walkDomain == null)
            {
                return NotFound();
            }

            //Convert Walk (Domain) in Walk DTO
            var walkDTO = _mapper.Map<Models.DTO.Walk>(walkDomain);

            //Retun DTO
            return Ok(walkDTO);
                
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //Request(DTO) to Domain Model
            var walkDomain = new Models.Domain.Walk()
            {               
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId
            };
            //Pass detail to Repository
            walkDomain = await _walkRepository.AddAsync(walkDomain);

            //Convert back to DTO hardway(without AutoMapper)
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //Return new item
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Get walk from database
            var walkDomain = await _walkRepository.DeleteAsync(id);

            //If null NotFound
            if (walkDomain == null)
            {
                return NotFound();
            }

            //Convert response back to DTO
            var walkDTO = _mapper.Map<Models.DTO.Walk>(walkDomain);

            //Return Ok response
            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //Convert DTO to Domain Model
            var walkDomain = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            //Update Walk using repository
            walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);

            //If null then return NotFound
            if (walkDomain == null)
            {
                return NotFound("Walk with this Id was not found");
            }

            //Convert Domain Back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //Return Ok response
            return Ok(walkDTO);
        }
    }
}
