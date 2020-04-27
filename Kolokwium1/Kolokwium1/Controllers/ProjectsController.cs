using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private IDbService _dbService;
        
        public ProjectsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{idProject}")]
        public IActionResult GetProjectData(int idProject)
        {
            return Ok(_dbService.GetProjectData(idProject));
        }
    }
}