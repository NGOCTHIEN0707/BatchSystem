using Domain.Logins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class LoginEntityTypeConfiguration : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.HasKey(x => x.LoginId);
            builder.Property(x => x.LoginId).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x=>x.FullName).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);



        }
    }
}
