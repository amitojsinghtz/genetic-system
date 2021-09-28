using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public class UserMap
    {
        //public UserMap(EntityTypeBuilder<User> entityBuilder)
        //{
        //    entityBuilder.HasKey(t => t.ID);
        //    entityBuilder.HasOne(t => t.PersonalInformation).WithOne(u => u.User).HasForeignKey<PersonalInformation>(x => x.ID);
        //    entityBuilder.HasOne(t => t.Branch).WithOne(u => u.User).HasForeignKey<Branch>(x => x.ID);
        //    entityBuilder.HasOne(t => t.Department).WithOne(u => u.User).HasForeignKey<Lookup>(x => x.ID);
        //    //entityBuilder.HasOne(t => t.Position).WithOne(u => u.User).HasForeignKey<Lookup>(x => x.ID);
        //    //entityBuilder.HasOne(t => t.EmployeeClass).WithOne(u => u.User).HasForeignKey<Lookup>(x => x.ID);
        //    //entityBuilder.HasOne(t => t.EmployeeType).WithOne(u => u.User).HasForeignKey<Lookup>(x => x.ID);
        //    //entityBuilder.HasOne(t => t.Role).WithOne(u => u.User).HasForeignKey<Lookup>(x => x.ID);
        //}
    }
}
