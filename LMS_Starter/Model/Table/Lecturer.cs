using System;
using System.Collections.Generic;

namespace LMS_Starter.Model
{
    public class Lecturer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Account Account { get; set; }
        public Address Address { get; set; }
        public ICollection<Teaching> Teaching { get; set; }

        public Lecturer() { }

        public static Lecturer LecturerMapping(Lecturer lecturer)
        {
            Lecturer newLecturer = new Lecturer
            {
                Email = lecturer.Email,
                FirstName = lecturer.FirstName,
                LastName = lecturer.LastName,
                Phone = lecturer.Phone,
                Address = Address.AddressMapping(lecturer.Address)
            };
            return newLecturer;
        }

        public bool ValidLecturer()
        {
            return Email != null && Phone != null;
        }

    }
}
