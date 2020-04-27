using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using kolos.DTOs.Requests;
using kolos.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolos.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IDbService _dbSevice;

        public AnimalsController()
        {
            _dbSevice = new SqlDbService();
        }

        [HttpGet]
        public IActionResult GetAnimals([FromQuery]string sortBy)
        {
            if (sortBy != "name" || sortBy != "type" || sortBy != "admissiondate" || sortBy != "lastname")
            {
                if (sortBy == null) return Ok(_dbSevice.GetAnimals("admissiondate"));
                return StatusCode(400);
            }
           
            return Ok(_dbSevice.GetAnimals(sortBy));
        }
        [HttpPost]
        public IActionResult AddAnimal(AddAnimalRequest request)
        {
            return Ok(_dbSevice.AddAnimal(request));
        }
    }
}