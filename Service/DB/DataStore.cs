using System;
using System.Collections.Generic;
using System.Linq;
using LMS_Starter.Model;
using LMS_Starter.Service;
using Microsoft.EntityFrameworkCore;

namespace LMS_Starter.Service
{
    public class DataStore:IDataStore
    {
        private DBContext _ctx;

        public DataStore(DBContext ctx)
        {
            _ctx = ctx;
        }
        // For Course API 
        public IEnumerable<Course> GetAllCourses()
        {
            return _ctx.Courses.OrderBy(course => course.ID).ToList();
        }

        public Course GetCourse(int courseID)
        {
            return _ctx.Courses
                       .Include(c => c.Teaching).ThenInclude(t => t.Lecturer)
                       .Include(s => s.Enrollments).ThenInclude(t => t.Student)
                       .SingleOrDefault(x => x.ID == courseID);
        }


        public void AddCourse(Course course)
        {
            _ctx.Courses.Add(course);
            Save();
        }

        public void EditCourse(int courseID, Course course)
        {
            var courseToEdit = _ctx.Courses.Find(courseID);
            courseToEdit.Name = course.Name;
            courseToEdit.CourseCode = course.CourseCode;
            courseToEdit.Description = course.Description;
            Save();
        }

        public void RemoveCourse(int Id)
        {
            _ctx.Courses.Remove(_ctx.Courses.Find(Id));
            Save();
        }

        // For Student API
        public IEnumerable<Student> GetAllStudents()
        {
            return _ctx.Students
                       .OrderBy(student => student.ID).ToList();
        }

        public Student GetStudent(int Id)
        {
            return _ctx.Students
                       .Include(student => student.Address)
                       .Include(s => s.Enrollments).ThenInclude(enrol => enrol.Course)
                       .SingleOrDefault(x => x.ID == Id);
        }


        public void AddStudent(Student student)
        {
            _ctx.Students.Add(student);
            Save();
        }

        public void EditStudent(int Id, Student student)
        {
            var studentToEdit = _ctx.Students.Find(Id);
            studentToEdit.Email = student.Email;
            studentToEdit.FirstName = student.FirstName;
            studentToEdit.LastName = student.LastName;
            studentToEdit.Phone = student.Phone;
            studentToEdit.Address = student.Address;
            Save();
        }

        public void RemoveStudent(int Id)
        {
            _ctx.Students.Remove(_ctx.Students.Find(Id));
            Save();
        }

        // For Lecturer API
        public IEnumerable<Lecturer> GetAllLecturers()
        {
            return _ctx.Lecturers
                       .OrderBy(lecturer => lecturer.ID).ToList();
        }

        public Lecturer GetLecturer(int Id)
        {
            return _ctx.Lecturers
                       .Include(lecturer => lecturer.Address)
                       .Include(s => s.Teaching).ThenInclude(enrol => enrol.Course)
                       .SingleOrDefault(x => x.ID == Id);
        }


        public void AddLecturer(Lecturer lecturer)
        {
            _ctx.Lecturers.Add(lecturer);
            Save();
        }

        public void EditLecturer(int Id, Lecturer lecturer)
        {
            var lecturerToEdit = _ctx.Lecturers.Find(Id);
            lecturerToEdit.Email = lecturer.Email;
            lecturerToEdit.FirstName = lecturer.FirstName;
            lecturerToEdit.LastName = lecturer.LastName;
            lecturerToEdit.Phone = lecturer.Phone;
            lecturerToEdit.Address = lecturer.Address;
            Save();
        }

        public void RemoveLecturer(int Id)
        {
            _ctx.Lecturers.Remove(_ctx.Lecturers.Find(Id));
            Save();
        }

        // For Enrolment API 
        public bool FindEnrolment(int studentId, int courseId) {
            var enrol = _ctx.Enrolments.Find(courseId, studentId);
            return enrol != null;
        }

        public void AddEnrolment(int studentId, int courseId)
        {
            Student student = _ctx.Students.Find(studentId);
            Course course = _ctx.Courses.Find(courseId);

            var newEnrol = new Enrolment { StudentId = studentId, CourseId = courseId };
            _ctx.Enrolments.Add(newEnrol);
            Save();
        }

        public void RemoveEnrolment(int studentID, int courseID)
        {
            var enrol = _ctx.Enrolments.Find(courseID, studentID);
            if (enrol != null)
            {
                _ctx.Enrolments.Remove(enrol);
            }
            Save();
        }

        // For Teaching API 
        public bool FindTeaching(int lecturerId, int courseId) {
            var teaching = _ctx.Teachings.Find(courseId, lecturerId);
            return teaching != null;
        }
        public void AddTeaching(int lecturerId, int courseId)
        {
            Lecturer lecturer = _ctx.Lecturers.Find(lecturerId);
            Course course = _ctx.Courses.Find(courseId);

            var newEnrol = new Teaching { LecturerId = lecturerId, CourseId = courseId };
            _ctx.Teachings.Add(newEnrol);
            Save();
        }

        public void RemoveTeaching(int lecturerId, int courseId)
        {
            var enrol = _ctx.Teachings.Find(courseId, lecturerId);
            if (enrol != null)
            {
                _ctx.Teachings.Remove(enrol);
            }
            Save();
        }

        // For User API
        public void AddAccount(Account account)
        {
            _ctx.Accounts.Add(account);
            Save();
        }

        public Account GetAccount(string email)
        {
            return _ctx.Accounts.Find(email);
        }

        public void EditAccount(string email, Account account)
        {
            var accountToEdit = _ctx.Accounts.Find(email);
            accountToEdit.Password = account.Password;
            accountToEdit.IsVerified = account.IsVerified;
            accountToEdit.VerificationCode = account.VerificationCode;
            accountToEdit.ExpireTime = account.ExpireTime;
            Save();
        }

        public bool Save()
        {
            //True for success , False should throw exception
            // change is number of row affected
            return (_ctx.SaveChanges() >= 0);
        }
    }
}
