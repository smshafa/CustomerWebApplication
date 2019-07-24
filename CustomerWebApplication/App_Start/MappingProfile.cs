
using AutoMapper;
using Customer.ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerWebApplication.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<DataLayer.Customer, CustomerViewModel>()
            //        .ForMember(d => d.FirstName,
            //            opt => opt.MapFrom(src => src.FirstName)
            //        )
            //        .ForMember(d => d.LastName,
            //            opt => opt.MapFrom(src => src.LastName)
            //        )
            //        .ForMember(d => d.ProvinceName,
            //            opt => opt.MapFrom(src => src.City.Province.ProvinceName)
            //        )
            //        .ForMember(d => d.CityName,
            //            opt => opt.MapFrom(src => src.City.CityName)
            //        );
            //});
            //Mapper.AssertConfigurationIsValid();



            CreateMap<Customer.DataLayer.Customer, CustomerViewModel>()
                .ForMember(d => d.FirstName,
                    opt => opt.MapFrom(src => src.FirstName)
                )
                .ForMember(d => d.LastName,
                    opt => opt.MapFrom(src => src.LastName)
                )
                .ForMember(d => d.ProvinceName,
                    opt => opt.MapFrom(src => src.City.Province.ProvinceName)
                )
                .ForMember(d => d.CityName,
                    opt => opt.MapFrom(src => src.City.CityName)
                )
                .ForMember(d => d.CityList,
                    opt => opt.Ignore()).ReverseMap()   // Accer is added this code. this extra list for dropDownList
                .ForMember(d => d.CustomerID,
                            opt => opt.Ignore()         // Accer is added this code. this extra list for gridView
                    ).ReverseMap();
                
        }
    }
}