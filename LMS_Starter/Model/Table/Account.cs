using System;
namespace LMS_Starter.Model
{
    public class Account
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int ID { get; set; }
        public bool IsVerified { get; set; }
        public string VerificationCode { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
        //public Account()
        //{
        //    IsVerified = false;
        //    Random generator = new Random();
        //    String r = generator.Next(100000, 1000000).ToString("D6");
        //    VerificationString = r;
        //}


        //public string GetVerificationString() {
        //    return VerificationString;
        //}
    }
}
