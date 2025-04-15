using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testingUK.Model.Dto;
using testingUK.Repositories;

namespace testingUK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrowseController : ControllerBase
    {
        private readonly IBrowseRepository browseRepository;
        private readonly IMapper mapper;

        public BrowseController(IBrowseRepository browseRepository , IMapper mapper)
        {
            this.browseRepository = browseRepository;
            this.mapper = mapper;
        }


        [HttpGet]
      public async Task<IActionResult> GetAllPublic()
        {
            var domain = await browseRepository.getAllPublic();

            var dto = mapper.Map<List<TripDto>>(domain);


            return Ok(dto);
        }


    }
}
