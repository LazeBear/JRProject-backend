using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LMS_Starter.Service;
using LMS_Starter.Model;

namespace LMS_Starter.Controllers
{
    [Produces("application/json")]
    [Route("api/course")]
    public class CourseController : Controller
    {
        private ILogger<CourseController> _LOGGER;
        private IDataStore _dbstore;
        //private IMailService _MailService;

        public CourseController(ILogger<CourseController> Logger, IDataStore db)
        {
            _LOGGER = Logger;
            _dbstore = db;
            //_MailService = MailService;

        }
        /// <summary>
        /// Get the List of Course
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get api/courses
        ///
        /// </remarks>
        /// <response code="200">Returns the list of course</response>
        /// <response code="500">Something goes wrong in server sides</response>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllCourses());
        }

        /// <summary>
        /// Get the Course by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get api/courses/id
        ///
        /// </remarks>
        /// <response code="200">Returns the required course</response>
        /// <response code="404">Course cannot found</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult result;
            var course = _dbstore.GetCourse(id);
            if (course != null)
            {
                result = Ok(course);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        /// <summary>
        /// Add a new cource
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post api/courses/id
        ///
        /// </remarks>
        /// <response code="200">Returns the required course</response>
        /// <response code="400">Invalid course object</response>
        [HttpPost]
        public IActionResult Post([FromBody] Course course)
        {
            if (course.ValidCourse())
            {
                _dbstore.AddCourse(Course.CourseMapping(course));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/course
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course newCourse)
        {
            var course = _dbstore.GetCourse(id);
            if (course != null)
            {
                if (newCourse.ValidCourse())
                {
                    _dbstore.EditCourse(id, Course.CourseMapping(newCourse));
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid course object!");
                }
            }
            else
            {
                return NotFound("ID not found");
            }
        }

        // DELETE api/course/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _dbstore.GetCourse(id);
            if (course != null)
            {
                _dbstore.RemoveCourse(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}


