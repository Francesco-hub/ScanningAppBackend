using Microsoft.AspNetCore.Mvc;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningAppRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertService _concertService;

        public ConcertsController(IConcertService concertService)
        {
            _concertService = concertService;
        }

        // GET api/concerts -- READ All
        [HttpGet]
        public ActionResult<IEnumerable<Concert>> Get()
        {
            return _concertService.GetAllConcerts();
        }

        // GET api/concerts/5 -- READ By id
        [HttpGet("{id}")]
        public ActionResult<Concert> Get(int id)
        {
            if (id < 1) return BadRequest("id must be greater then 0");

            return _concertService.FindConcertsById(id);
        }
    }
}
