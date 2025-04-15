using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testingUK.Model;
using testingUK.Model.Dto;
using testingUK.Repositories;

namespace testingUK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TravelTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITravelTypeRepository travelTypeRepository;

        public TravelTypeController(IMapper mapper ,
            ITravelTypeRepository travelTypeRepository)
        {
            this.mapper = mapper;
            this.travelTypeRepository = travelTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var domainTravelType = await travelTypeRepository.GetAll();
            var dtoTravelType = mapper.Map<List<TravelTypeDto>>(domainTravelType);
            return Ok(domainTravelType);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
           var domainTravelType = await travelTypeRepository.GetById(Id);

            if (domainTravelType == null) return NotFound();

            var dtoTravelType = mapper.Map<TravelTypeDto>(domainTravelType);

            return Ok(dtoTravelType);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] TravelTypeDto travelTypeDto)
        {
            var domainTravelType = mapper.Map<TravelType>(travelTypeDto);
            domainTravelType = await travelTypeRepository.Create(domainTravelType);
            travelTypeDto = mapper.Map<TravelTypeDto>(domainTravelType);

            return CreatedAtAction(nameof(GetById), new { id = domainTravelType.Id }, travelTypeDto);

        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var domain = await travelTypeRepository.Delete(Id);

            if(domain==null)
            {
                return NotFound();
            }

            var dto = mapper.Map<TravelTypeDto>(domain);

            return Ok();

        }

        [HttpPatch]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid Id 
            , [FromBody] AddTravelTypeDto addTravelTypeDto)
        {
            var domainTravelType = await travelTypeRepository.Update(Id, addTravelTypeDto);

            var dtoTravelType = mapper.Map<TravelTypeDto>(domainTravelType);

            return Ok(dtoTravelType);
        }


    }
}
