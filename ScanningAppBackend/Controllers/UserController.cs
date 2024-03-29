﻿using Microsoft.AspNetCore.Mvc;
using ScanningApp.Core.ApplicationService;
using ScanningApp.Core.Entity;
using System.Collections.Generic;

namespace ScanningAppRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/user -- READ All
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userService.GetAllUsers());
        }
    }
}
