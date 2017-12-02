using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LMS_Starter.Service;
using LMS_Starter.Model;


namespace LMS_Starter
{
    [Produces("application/json")]
    [Route("api/lecturer")]
    public class LecturerController : Controller
    {
		private ILogger<LecturerController> _LOGGER;
        private IDataStore _dbstore;
		//private IMailService _MailService;

        public LecturerController(ILogger<LecturerController> Logger, IDataStore db)
		{
			_LOGGER = Logger;
            _dbstore = db;
			//_MailService = MailService;

		}
		// GET: api/lecturer
		[HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllLecturers());
        }

		// GET api/lecturer/id
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
            IActionResult result;
            var lecturer = _dbstore.GetLecturer(id);
            if (lecturer != null)
            {
                result = Ok(lecturer);
            }
            else
            {
                result = NotFound();
            }
            return result;
		}

		// POST api/lecturer
		[HttpPost]
		public IActionResult Post([FromBody] Lecturer lecturer)
		{
            if (lecturer.ValidLecturer())
            {
                _dbstore.AddLecturer(Lecturer.LecturerMapping(lecturer));
                return Ok();
            }
            else
            {
                return BadRequest("Invalid lecturer object!");
            }
		}

		// PUT api/lecturer
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Lecturer newLecturer)
		{
            var lecturer = _dbstore.GetLecturer(id);
            if (lecturer != null)
            {
                if (newLecturer.ValidLecturer())
                {
                    _dbstore.EditLecturer(id, Lecturer.LecturerMapping(newLecturer));
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid lecturer object!");
                }
            }
            else
            {
                return NotFound("ID not found");
            }
		}

		// DELETE api/lecturer/id
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
            var lecturer = _dbstore.GetLecturer(id);
            if (lecturer != null)
            {
                _dbstore.RemoveLecturer(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
		}
    }
}
