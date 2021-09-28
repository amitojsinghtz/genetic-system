using Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
   public class PersonalInformationMap
    {
        //public PersonalInformationMap(EntityTypeBuilder<PersonalInformation> entityBuilder)
        //{
        //    entityBuilder.HasKey(t => t.ID);
        //    entityBuilder.HasOne(t => t.Country).WithOne(u => u.PersonalInformation).HasForeignKey<Country>(x => x.ID);
        //    entityBuilder.HasOne(t => t.City).WithOne(u => u.PersonalInformation).HasForeignKey<City>(x => x.ID);
        //}
    }
}
