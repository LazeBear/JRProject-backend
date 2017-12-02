using System;
using System.Collections.Generic;
using LMS_Starter.Model;

namespace LMS_Starter.Service
{
    public interface IDataStore
    {
        // courses
        IEnumerable<Course> GetAllCourses();
        Course GetCourse(int courseID);
        void AddCourse(Course course);
        void EditCourse(int courseID, Course course);
        void RemoveCourse(int id);

        // students
        IEnumerable<Student> GetAllStudents();
        Student GetStudent(int ID);
        void AddStudent(Student student);
        void EditStudent(int ID, Student student);
        void RemoveStudent(int id);

        // lecturers
        IEnumerable<Lecturer> GetAllLecturers();
        Lecturer GetLecturer(int ID);
        void AddLecturer(Lecturer lecturer);
        void EditLecturer(int ID, Lecturer lecturer);
        void RemoveLecturer(int id);

        // enrolments
        bool FindEnrolment(int studentId, int courseId);
        void AddEnrolment(int studentId, int courseId);
        void RemoveEnrolment(int studentID, int courseID);

        //teaching
        bool FindTeaching(int lecturerId, int courseId);
        void AddTeaching(int lecturerId, int courseId);
        void RemoveTeaching(int lecturerId, int courseId);

        // user
        void AddAccount(Account account);
        Account GetAccount(string email);
        void EditAccount(string email, Account account);
    }
}
