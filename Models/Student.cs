using System;
using System.Collections.Generic;

namespace HighSchool.Models
{
    public partial class Student
    {
        public Student()
        {
            Grades = new HashSet<Grade>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Ssn { get; set; } = null!;
        public string Class { get; set; } = null!;

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
