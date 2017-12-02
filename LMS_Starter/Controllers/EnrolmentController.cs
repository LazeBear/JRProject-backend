using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS_Starter.Model.DTO;
using LMS_Starter.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS_Starter.Controllers
{
    [Produces("application/json")]
    [Route("api/enrolment")]
    public class EnrolmentController : Controller
    {
        private IDataStore _dbstore;
        public EnrolmentController(IDataStore dbstore)
        {
            _dbstore = dbstore;
        }
        // POST api/values
        [HttpPost("{sid}/{cid}")]
        public IActionResult Post(int sid, int cid)
        {
            if (checkValue(sid, cid)) {
                if (_dbstore.FindEnrolment(sid,cid)) {
                    return BadRequest("Enrolment already exists!");
                }
                _dbstore.AddEnrolment(sid, cid);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{sid}/{cid}")]
        public IActionResult Delete(int sid, int cid)
        {
            if (checkValue(sid,cid))
            {
                _dbstore.RemoveEnrolment(sid, cid);
                return Ok();
            }
            return NotFound();
        }

        private bool checkValue(int sid, int cid) {
            // if student id or course id does not exist, reject
            return (_dbstore.GetStudent(sid) != null && _dbstore.GetCourse(cid) != null);
        }
    }
}
