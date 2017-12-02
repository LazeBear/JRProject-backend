using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LMS_Starter.Service;
using LMS_Starter.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS_Starter.Controllers
{
    [Produces("application/json")]
    [Route("api/student")]
    public class StudentController : Controller
    {
        private ILogger<StudentController> _LOGGER;
        private IDataStore _dbstore;
        //private IMailService _MailService;

        public StudentController(ILogger<StudentController> Logger, IDataStore db)
        {
            _LOGGER = Logger;
            _dbstore = db;
            //_MailService = MailService;

        }
        // GET: api/student
        [HttpGet]
        /**
         * Returns all student results
         */
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllStudents());
        }

        // GET api/student/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult result;
            var student = _dbstore.GetStudent(id);
            if (student != null)
            {
                result = Ok(student);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // POST api/student
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student.ValidStudent())
            {
                _dbstore.AddStudent(Student.StudentMapping(student));
                return Ok();
            }
            else
            {
                return BadRequest("Invalid student object!");
            }
        }

        // PUT api/student
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Student newStudent)
        {
            var student = _dbstore.GetStudent(id);
            if (student != null)
            {
                if (newStudent.ValidStudent())
                {
                    _dbstore.EditStudent(id, Student.StudentMapping(newStudent));
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid student object!");
                }
            }
            else
            {
                return NotFound("ID not found");
            }
        }

        // DELETE api/student/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _dbstore.GetStudent(id);
            if (student != null)
            {
                _dbstore.RemoveStudent(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
