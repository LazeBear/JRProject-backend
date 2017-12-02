using System;
namespace LMS_Starter.Model
{
    public class Teaching
    {
        public int LecturerId { get; set; }
        public int CourseId { get; set; }
        public Lecturer Lecturer { get; set; }
        public Course Course { get; set; }
        public Teaching()
        {
        }
    }
}
