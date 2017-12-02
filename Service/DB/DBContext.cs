using System;
using LMS_Starter.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LMS_Starter.Service
{
    public class DBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Teaching> Teachings { get; set; }

        private ServiceConfiguration _DBConfig;

        public DBContext(DbContextOptions<DBContext> options, IOptions<ServiceConfiguration> DBConfiguration) : base(options)
        {
            //Database.EnsureCreated();
            _DBConfig = DBConfiguration.Value;

            Database.Migrate();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // user
            modelBuilder.Entity<Account>()
                        .HasKey(a => a.Email);

            modelBuilder.Entity<Account>()
                        .Property(a => a.ID).ValueGeneratedOnAdd();

            //// these two lines should be moved
            //modelBuilder.Entity<Account>().HasOne(a => a.Student)
            //           .WithOne(s => s.Account)
            //            .HasForeignKey<Student>(s => s.Email);

            //modelBuilder.Entity<Account>().HasOne(a => a.Lecturer)
                       //.WithOne(l => l.Account)
                        //.HasForeignKey<Lecturer>(l => l.Email);
            
            //Course
            // increase ID every time when added in
            modelBuilder.Entity<Course>()
                        .Property(a => a.ID).ValueGeneratedOnAdd();

            // set primary key
            modelBuilder.Entity<Course>()
                        .HasKey(a => a.ID);

            //Lecturer
            modelBuilder.Entity<Lecturer>()
                       .Property(a => a.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Lecturer>()
                        .HasKey(a => a.ID);

            //// student - account
            //modelBuilder.Entity<Lecturer>().HasOne(l => l.Account)
                        //.WithOne(a => a.Lecturer)
                        //.HasForeignKey<Account>(a => a.LecturerId);

            //Student
            modelBuilder.Entity<Student>()
                       .Property(a => a.ID).ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>()
                        .HasKey(a => a.ID);

            // address
            modelBuilder.Entity<Address>()
            .Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Address>()
                        .HasKey(a => a.Id);

            // lecturer - address
            modelBuilder.Entity<Lecturer>().HasOne(lecturer => lecturer.Address)
                        .WithOne(lAddress => lAddress.Lecturer)
                        .HasForeignKey<Address>(lAddress => lAddress.LecturerId);
            
            // student - address, 1-1
            modelBuilder.Entity<Student>().HasOne(student => student.Address)
                        .WithOne(stdAddress => stdAddress.Student)
                        .HasForeignKey<Address>(stdAddress => stdAddress.StudentId);

            //// student - account
            //modelBuilder.Entity<Student>().HasOne(student => student.Account)
                        //.WithOne(a => a.Student)
                        //.HasForeignKey<Account>(a => a.StudentId);


            // Enrolment
            modelBuilder.Entity<Enrolment>()
                        .HasKey(a => new { a.CourseId, a.StudentId }); //Remeber the Order


            modelBuilder.Entity<Enrolment>()
                        .HasOne(bc => bc.Course)
                        .WithMany(b => b.Enrollments)
                        .HasForeignKey(bc => bc.CourseId);

            modelBuilder.Entity<Enrolment>()
                        .HasOne(bc => bc.Student)
                        .WithMany(b => b.Enrollments)
                        .HasForeignKey(bc => bc.StudentId);

            // Teaching
            modelBuilder.Entity<Teaching>()
                        .HasKey(a => new { a.CourseId, a.LecturerId }); //Remeber the Order


            modelBuilder.Entity<Teaching>()
                        .HasOne(bc => bc.Course)
                        .WithMany(b => b.Teaching)
                        .HasForeignKey(bc => bc.CourseId);

            modelBuilder.Entity<Teaching>()
                        .HasOne(bc => bc.Lecturer)
                        .WithMany(b => b.Teaching)
                        .HasForeignKey(bc => bc.LecturerId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // DB setting is set here 
            //var connectionString = "Server=tcp:lazebear.database.windows.net,1433;Initial Catalog=webproject;Persist Security Info=False;User ID=mason;Password=Xiong123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(_DBConfig.DBConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}

