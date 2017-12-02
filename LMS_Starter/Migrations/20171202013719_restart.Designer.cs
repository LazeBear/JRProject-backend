﻿// <auto-generated />
using LMS_Starter.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LMS_Starter.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20171202013719_restart")]
    partial class restart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LMS_Starter.Model.Account", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpireTime");

                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsVerified");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<string>("Token");

                    b.Property<string>("VerificationCode");

                    b.HasKey("Email");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LMS_Starter.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<int?>("LecturerId");

                    b.Property<string>("Line1");

                    b.Property<string>("Line2");

                    b.Property<int>("PostCode");

                    b.Property<string>("State");

                    b.Property<int?>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId")
                        .IsUnique()
                        .HasFilter("[LecturerId] IS NOT NULL");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("LMS_Starter.Model.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseCode");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("LMS_Starter.Model.Enrolment", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("StudentId");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrolments");
                });

            modelBuilder.Entity("LMS_Starter.Model.Lecturer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountEmail");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.HasKey("ID");

                    b.HasIndex("AccountEmail");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("LMS_Starter.Model.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountEmail");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.HasKey("ID");

                    b.HasIndex("AccountEmail");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LMS_Starter.Model.Teaching", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<int>("LecturerId");

                    b.HasKey("CourseId", "LecturerId");

                    b.HasIndex("LecturerId");

                    b.ToTable("Teachings");
                });

            modelBuilder.Entity("LMS_Starter.Model.Address", b =>
                {
                    b.HasOne("LMS_Starter.Model.Lecturer", "Lecturer")
                        .WithOne("Address")
                        .HasForeignKey("LMS_Starter.Model.Address", "LecturerId");

                    b.HasOne("LMS_Starter.Model.Student", "Student")
                        .WithOne("Address")
                        .HasForeignKey("LMS_Starter.Model.Address", "StudentId");
                });

            modelBuilder.Entity("LMS_Starter.Model.Enrolment", b =>
                {
                    b.HasOne("LMS_Starter.Model.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS_Starter.Model.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LMS_Starter.Model.Lecturer", b =>
                {
                    b.HasOne("LMS_Starter.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountEmail");
                });

            modelBuilder.Entity("LMS_Starter.Model.Student", b =>
                {
                    b.HasOne("LMS_Starter.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountEmail");
                });

            modelBuilder.Entity("LMS_Starter.Model.Teaching", b =>
                {
                    b.HasOne("LMS_Starter.Model.Course", "Course")
                        .WithMany("Teaching")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LMS_Starter.Model.Lecturer", "Lecturer")
                        .WithMany("Teaching")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
