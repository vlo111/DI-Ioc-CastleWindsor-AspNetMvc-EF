using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CastleWindsorTestApp.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Location { get; set; }
        public ICollection<Employee> Employees { set; get; }
    }

}