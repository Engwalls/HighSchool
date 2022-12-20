using System;
using System.Collections.Generic;

namespace HighSchool.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public string Subject { get; set; } = null!;
        public int Grade1 { get; set; }
        public DateTime SetDate { get; set; }
        public int FkStudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public int FkEmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;

        public virtual Employee FkEmployee { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
