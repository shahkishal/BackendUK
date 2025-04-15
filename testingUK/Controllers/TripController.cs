using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using testingUK.Model;
using testingUK.Model.Dto;
using testingUK.Repositories;

namespace testingUK.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="User,Admin")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITripRepository tripRepository;

        public TripController(IMapper mapper , ITripRepository tripRepository)
        {
            this.mapper = mapper;
            this.tripRepository = tripRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sortBy,
                                                [FromQuery] bool?  isAscending ,
                                                [FromQuery] int pageNumber=1 ,
                                                [FromQuery] int pageSize=1000
                                                )
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return BadRequest("Please Login To Continue ... ");
            }


            var TripRegionData = await tripRepository.GetAll(filterOn, filterQuery, sortBy, isAscending??true,pageNumber , pageSize , userId);
            //var TripRegion = TripRegionData.Data;

            //var TripDto = mapper.Map<List<TripDto>>(TripRegion);

            return Ok(TripRegionData);

        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var DomainTrip = await tripRepository.GetById(Id);

            if(DomainTrip==null)
            {
                return NotFound();
            }

            var DtoTrip = mapper.Map<TripDto>(DomainTrip);

            return Ok(DtoTrip);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddTripDto trip)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return BadRequest("Please Login To Continue ... ");
            }

            var domainTrip = mapper.Map<Trip>(trip);

            domainTrip.UserId = userId;

            domainTrip = await tripRepository.Create(domainTrip);

            

            var dtoTrip = mapper.Map<TripDto>(domainTrip);

            return CreatedAtAction(nameof(GetById), new { id = domainTrip.Id }, dtoTrip);

        }

        [HttpPatch]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id , AddTripDto Trip)
        {
            var domainTrip = await tripRepository.Update(Id, Trip);

            if (domainTrip == null) return NotFound();

            var dtoTrip = mapper.Map<TripDto>(domainTrip);

            return Ok(dtoTrip);

        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var domainTrip = await tripRepository.Delete(Id);

            if (domainTrip == null) return NotFound();

            var dtoTrip = mapper.Map<TripDto>(domainTrip);

            return Ok(dtoTrip);
        }
        

    }
}
