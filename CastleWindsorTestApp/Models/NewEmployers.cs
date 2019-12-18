using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CastleWindsorTestApp.Models
{
    public static class NewEmployers
    {
        public static List<Department> departments = new List<Department>
                {
                    new Department { DepartmentName = "FINANCE", Location = "SYDNEY" },
                    new Department { DepartmentName = "AUDIT", Location = "MELBOURNE" },
                    new Department { DepartmentName = "PRODUCTION", Location = "BRISBANE" }
                };
        public static IEnumerable<Employee> employers = new List<Employee>
                {
                    new Employee { EmployeeName = "KAYLING", JobName = "PRESIDENT", Salary = "6000", Department = departments[0], DepartmentId = 1 },
                    new Employee { EmployeeName = "BLAZE", JobName = "MANAGER", Salary = "2750", Department = departments[1], DepartmentId = 2 },
                    new Employee { EmployeeName = "CLARE", JobName = "MANAGER", Salary = "2950", Department = departments[0], DepartmentId = 1 },
                    new Employee { EmployeeName = "JONAS", JobName = "MANAGER", Salary = "2900", Department = departments[2], DepartmentId = 3 },
                    new Employee { EmployeeName = "SCARLET", JobName = "ANALYST ", Salary = "3100", Department = departments[2], DepartmentId = 3 },
                    new Employee { EmployeeName = "FRANK", JobName = "SALESMAN", Salary = "1700", Department = departments[0], DepartmentId = 1 },
                    new Employee { EmployeeName = "JOSE", JobName = "SALESMAN", Salary = "1000", Department = departments[1], DepartmentId = 2 },
                    new Employee { EmployeeName = "DAVID", JobName = "PRESIDENT", Salary = "6000", Department = departments[2], DepartmentId = 2 },
                    new Employee { EmployeeName = "SIA", JobName = "ANALYST", Salary = "5000", Department = departments[0], DepartmentId = 2 },
                    new Employee { EmployeeName = "ROBERTO", JobName = "Programmer", Salary = "4000", Department = departments[2], DepartmentId = 2 },
                    new Employee { EmployeeName = "ANGELINA", JobName = "SALESMAN", Salary = "3000", Department = departments[0], DepartmentId = 2 },
                    new Employee { EmployeeName = "SABINA", JobName = "CLERK", Salary = "950", Department = departments[1], DepartmentId = 2 },
                    new Employee { EmployeeName = "MICHEL", JobName = "MANAGER", Salary = "2500", Department = departments[2], DepartmentId = 2 },
                    new Employee { EmployeeName = "SANDRA", JobName = "PRESIDENT", Salary = "6500", Department = departments[1], DepartmentId = 2 },
                    new Employee { EmployeeName = "ADRIANA", JobName = "CLERK", Salary = "1000", Department = departments[0], DepartmentId = 2 },
                    new Employee { EmployeeName = "CAMILA", JobName = "ANALYST", Salary = "3000", Department = departments[2], DepartmentId = 2 }
                };
    }
}