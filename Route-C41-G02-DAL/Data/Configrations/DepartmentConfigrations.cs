using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route_C41_G02_DAL.Models;

namespace Route_C41_G02_DAL.Data.Configrations
{
    internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Fluent APIs for Department
            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
            builder.Property(D => D.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(D => D.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }
}
