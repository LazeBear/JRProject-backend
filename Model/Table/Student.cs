using System;
using System.Collections.Generic;

namespace LMS_Starter.Model
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public Account Account { get; set; }
        public Address Address { get; set; }
        public ICollection<Enrolment> Enrollments { get; set; }

        public Student() { }

        public static Student StudentMapping(Student student)
        {
            Student newStudent = new Student
            {
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Phone = student.Phone,
                Address = Address.AddressMapping(student.Address)
            };
            return newStudent;
        }

        public bool ValidStudent()
        {
            return Email != null && Phone != null;
        }
    }
}
