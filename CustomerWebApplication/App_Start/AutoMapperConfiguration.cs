using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Customer.ServiceLayer.App_Start;

namespace CustomerWebApplication.App_Start
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