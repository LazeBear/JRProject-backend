using System;
namespace LMS_Starter.Model
{
    public class Verification
    {
        public string Email;
        public string Code;
        public bool ValidInfo(){
            return Email != null && Code != null;
        }
    }
}
