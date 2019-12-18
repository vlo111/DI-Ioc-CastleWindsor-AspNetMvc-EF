using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CastleWindsorTestApp.Models.Mapping
{
    internal class DepartmentEntityConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentEntityConfiguration()
        {
            // Primary Key
            this.HasKey(p => p.DepartmentId);

            // Properties
            this.Property(p => p.DepartmentName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mapping
            this.ToTable("department");
            this.Property(p => p.DepartmentId).HasColumnName("dep_id");
            this.Property(p => p.DepartmentName).HasColumnName("dep_name");
            this.Property(p => p.Location).HasColumnName("dep_location");

        }
    }
}