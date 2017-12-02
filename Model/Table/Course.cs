using System;
using System.Collections.Generic;

namespace LMS_Starter.Model
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public string Description { set; get; }
        public ICollection<Enrolment> Enrollments { get; set; }
        public ICollection<Teaching> Teaching { get; set; }

        public Course() { }

        public static Course CourseMapping(Course courseFromPost)
        {
            Course newCourse = new Course
            {
                //Mapping value in here
                //Ignore the ID
                Name = courseFromPost.Name,
                Description = courseFromPost.Description,
                CourseCode = courseFromPost.CourseCode
            };
            return newCourse;
        }

        public bool ValidCourse()
        {
            return (Name != null) && (CourseCode != null) && (Description != null);
        }
    }
}
