using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium1.DTOs.Requests;
using Kolokwium1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private IDbService _dbService;

        public TaskController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public IActionResult AddTask(AddTaskRequest request)
        {
            return Ok(_dbService.AddTask(request));
        }
    }
}