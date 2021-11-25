using System.Collections.Generic;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.Entity;
using Microsoft.AspNetCore.Mvc;

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
            ///Customers with all there orders? NO
            ///
            return _concertService.GetAllConcerts();
        }

        // GET api/concerts/5 -- READ By Id
        [HttpGet("{id}")]
        public ActionResult<Concert> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0");

            //return _concertService.FindConcertById(id);
            return _concertService.FindConcertByIdIncludeScans(id);
        }

    }
}
