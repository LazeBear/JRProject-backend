using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LMS_Starter.Model;
using LMS_Starter.Service;
using Newtonsoft.Json.Linq;
using LMS_Starter.Model.DTO;

namespace LMS_Starter.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController:Controller
    {
        private ILogger<UserController> _LOGGER;
        private IMailService _MailService;
        private ISMSService _SMSService;
        private IDataStore _dbstore;

        public UserController(ILogger<UserController> Logger, IMailService mailService, ISMSService smsService, IDataStore db)
        {
            _LOGGER = Logger;
            _MailService = mailService;
            _SMSService = smsService;
            _dbstore = db;
        }

        // GET api/user/sample
        [HttpGet("sample")]
        public IActionResult Get()
        {
            NewUser result = new NewUser
            {
                Email = "student@example.com",
                Phone = "+61412345678",
                Password = "123"
            };
            //string verifSample= "{\"Username\":\"s12345\",\"Code\":\"123456\"}";
            return Ok(result);
        }

        // POST api/user/register
        [HttpPost("register")]
        public IActionResult Post([FromBody] NewUser newUser)
        {
            if (!newUser.ValidNewUser())
            {
                return BadRequest("Invalid register information!");
            }
            Account account = new Account {
                Email = newUser.Email,
                Password = newUser.Password,
                IsVerified = false,
            };
            var exist = _dbstore.GetAccount(account.Email);
            if(exist != null) {
                return BadRequest("Email already exist!");
            }
            _dbstore.AddAccount(account);
            String msg = "Thank you for register with us!\n" +
                "Your login user name is: " + account.Email + "\n" +
                                                   "Your password is: " + account.Password;
            _MailService.Send(newUser.Email, "New user", "Welcome!", msg);
            return Ok();
        }

        //// GET api/user/verification/{email}
        //[HttpGet("verification/{email}")]
        //public IActionResult GetVerificationRequest(string email)
        //{
        //    var account = _dbstore.GetAccount(email);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }
        //    account.ExpireTime = DateTime.Now.AddSeconds(600);
        //    string code = GenerateCode();
        //    account.VerificationCode = code;
        //    string msg = $"Your verification code is: {code}\\nIt will expire in 600 seconds.";
        //    string phone;
        //    if(account.UserType == UserType.STUDENT) {
        //        phone = _dbstore.GetStudent((int)account.StudentId).Phone;
        //    } else {
        //        phone = _dbstore.GetLecturer((int)account.LecturerId).Phone;
        //    }
        //    _SMSService.SendSMS(phone, msg);
        //    _dbstore.EditAccount(email, account);
        //    return Ok();
        //}

        //// POST api/user/verification
        //[HttpPost("verification")]
        //public IActionResult PostVerification([FromBody]Verification data) {
        //    if(!data.ValidInfo()) {
        //        return BadRequest("Invalid verification information!");
        //    }
        //    var account = _dbstore.GetAccount(data.Email);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }
        //    if(account.VerificationCode.Equals(data.Code) && DateTime.Compare(DateTime.Now, account.ExpireTime) < 0 ){
        //        account.IsVerified = true;
        //        _dbstore.EditAccount(account.Email, account);
        //        return Ok();
        //    }
        //    return BadRequest("Verification code does not match!");
        //}

        private String GenerateCode() {
            Random generator = new Random();
            String r = generator.Next(100000, 1000000).ToString("D6");
            return r;
        }
    }
}
