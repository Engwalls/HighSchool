using System;
using System.Collections.Generic;

namespace HighSchool.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Grades = new HashSet<Grade>();
        }

        public int EmployeeId { get; set; }
        public string Title { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Ssn { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
