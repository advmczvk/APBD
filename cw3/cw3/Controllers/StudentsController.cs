using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw3.DAL;
using Microsoft.AspNetCore.Mvc;


namespace cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("{index}")]
        public IActionResult GetStudents(string index)
        {
            return Ok(_dbService.GetStudents(index));
        }
        [HttpGet("getEnrollment/{index}")]
        public IActionResult getStudentEnrollment(string index)
        {
            return Ok(_dbService.GetStudentEnrollment(index));
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 2000)}";
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(string id, string varType, string value)
        {   
            return Ok($"Zaktualizowano studenta {_dbService.UpdateStudent(id, varType, value)}");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _dbService.DeleteStudent(id);
            return Ok($"Usunięto studenta o id: {id}");
        }
    }
}