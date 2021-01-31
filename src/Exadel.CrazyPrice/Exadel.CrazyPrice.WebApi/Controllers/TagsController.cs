﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    [ApiController]
    [Route("tags")]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private static readonly string[] _tags = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TagsController> _logger;

        public TagsController(ILogger<TagsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "moderator, admin")]
        public IEnumerable<string> Get()
        {            
            
            return _tags;
        }
    }
}
