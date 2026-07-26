using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreProject.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public int EmployeeSsn { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeAge { get; set; }
        public double EmployeeSalary { get; set; }
    }
}
