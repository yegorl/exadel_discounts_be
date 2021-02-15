using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Interfaces;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IUserRepository _userRepository;
        public StatisticController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> GetUsersStatistic()
        {
            return Ok();
        }
    }
}
