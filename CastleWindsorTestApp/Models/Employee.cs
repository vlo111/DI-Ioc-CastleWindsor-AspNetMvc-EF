using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CastleWindsorTestApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter the name.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Please enter the job.")]
        public string JobName { get; set; }

        [Required(ErrorMessage = "Please enter the salary.")]
        public string Salary { get; set; }
        [Required(ErrorMessage = "Please enter the department.")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }

}