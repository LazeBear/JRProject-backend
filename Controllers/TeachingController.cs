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
    [Route("api/teaching")]
    public class TeachingController : Controller
    {
        private IDataStore _dbstore;
        public TeachingController(IDataStore dbstore)
        {
            _dbstore = dbstore;
        }
        // POST api/values
        [HttpPost("{lid}/{cid}")]
        public IActionResult Post(int lid, int cid)
        {
            if (checkValue(lid, cid))
            {
                if (_dbstore.FindTeaching(lid, cid)) {
                    return BadRequest("Teaching relation exists!");
                }
                _dbstore.AddTeaching(lid, cid);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{lid}/{cid}")]
        public IActionResult Delete(int lid, int cid)
        {
            if (checkValue(lid, cid))
            {
                _dbstore.RemoveTeaching(lid, cid);
                return Ok();
            }
            return NotFound();
        }

        private bool checkValue(int lid, int cid)
        {
            
            // if student id or course id does not exist, reject
            return (_dbstore.GetLecturer(lid) != null && _dbstore.GetCourse(cid) != null);
        }
    }
}
