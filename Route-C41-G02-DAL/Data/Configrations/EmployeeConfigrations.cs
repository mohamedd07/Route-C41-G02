using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route_C41_G02_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_C41_G02_DAL.Data.Configrations
{
    internal class EmployeeConfigrations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            // Fluent APIs for "Employee"
            builder.Property(E => E.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(E=>E.Address).IsRequired();

            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");

            builder.Property(E => E.Gender)
                .HasConversion(
                (Gender) => Gender.ToString(),
                (genderAsStr) =>(Gender) Enum.Parse(typeof(Gender), genderAsStr)
                );

        }
    }
}
