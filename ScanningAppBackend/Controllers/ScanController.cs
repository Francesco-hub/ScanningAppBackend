using Microsoft.AspNetCore.Mvc;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.Entity;
using System;
using System.Collections.Generic;

namespace ScanningAppRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScansController : ControllerBase
    {
        private readonly IScanService _scanService;

        public ScansController(IScanService scanService)
        {
            _scanService = scanService;
        }

        // GET api/scans -- READ All
        [HttpGet]
        public ActionResult<IEnumerable<Scan>> Get()
        {
            return Ok(_scanService.GetAllScans());
        }

        // GET api/scan/5 -- READ By id
        [HttpGet("{id}")]
        public ActionResult<Scan> Get(int id)
        {
            if (id < 1) return BadRequest("id must be greater then 0");

            return Ok();
        }

        // POST api/scan -- CREATE
        [HttpPost]
        public ActionResult<Scan> Post([FromBody] List<Scan> scanList)
        {
            try
            {
                return Ok(_scanService.CreateScans(scanList));
            }
            catch (Exception e)
            {
               return BadRequest(e.InnerException.Message);
            }
        }
    }
}