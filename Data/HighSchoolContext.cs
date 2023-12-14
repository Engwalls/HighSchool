using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HighSchool.Models;

namespace HighSchool.Data
{
    public partial class HighSchoolContext : DbContext
    {
        public HighSchoolContext()
        {
        }

        public HighSchoolContext(DbContextOptions<HighSchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source= ; Initial Catalog=HighSchool;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HireDate).HasColumnType("date");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Ssn)
                    .HasMaxLength(12)
                    .HasColumnName("SSN");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.EmployeeName).HasMaxLength(50);

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.Property(e => e.Grade1).HasColumnName("Grade");

                entity.Property(e => e.SetDate).HasColumnType("date");

                entity.Property(e => e.StudentName).HasMaxLength(50);

                entity.Property(e => e.Subject).HasMaxLength(30);

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Employee");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Class).HasMaxLength(15);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Ssn)
                    .HasMaxLength(12)
                    .HasColumnName("SSN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
