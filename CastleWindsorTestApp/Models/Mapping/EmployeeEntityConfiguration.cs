using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CastleWindsorTestApp.Models.Mapping
{
    internal class EmployeeEntityConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeEntityConfiguration()
        {
            // Primary Key
            this.HasKey<int>(p => p.EmployeeId);

            // Properties
            this.Property(p => p.EmployeeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("employee");
            this.Property(p => p.EmployeeId).HasColumnName("emp_id");
            this.Property(p => p.EmployeeName).HasColumnName("emp_name");
            this.Property(p => p.JobName).HasColumnName("job_name");
            this.Property(p => p.Salary).HasColumnName("salary");
            this.Property(p => p.DepartmentId).HasColumnName("dep_id");


            // Relationships
            this.HasRequired(p => p.Department)
                .WithMany(p => p.Employees)
                .HasForeignKey(p => p.DepartmentId);
        }
    }
}