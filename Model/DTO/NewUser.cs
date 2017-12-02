using System;
namespace LMS_Starter.Model.DTO
{
    public class NewUser
    {
        public string Email;
        public string Password;
        public string Phone;
        public NewUser()
        {
            
        }
        public bool ValidNewUser() {
            return Email != null && Password != null && Phone != null;
        }
    }
}
