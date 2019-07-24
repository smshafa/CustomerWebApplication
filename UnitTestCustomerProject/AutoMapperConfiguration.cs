using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace UnitTestCustomerProject
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(
                c =>
                {
                    c.AddProfile<MappingProfile>();
                });
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}