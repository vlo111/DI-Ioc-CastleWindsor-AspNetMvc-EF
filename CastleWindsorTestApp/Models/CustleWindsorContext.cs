namespace CastleWindsorTestApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using CastleWindsorTestApp.Models.Mapping;

    public partial class CustleWindsorContext : DbContext
    {
        public CustleWindsorContext()
            : base("name=CustleWindsorContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeEntityConfiguration());
            modelBuilder.Configurations.Add(new DepartmentEntityConfiguration());
        }
    }
}
