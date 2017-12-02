﻿using System;
namespace LMS_Starter.Model
{
    public class Enrolment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Enrolment()
        {
        }
    }
}
