using AutoMapper;
using Customer.DataLayer;
using Customer.ServiceLayer.ViewModels;

namespace UnitTestCustomerProject
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



            //CreateMap<Customer.DataLayer.Customer, CustomerViewModel>()
            //    .ForMember(d => d.FirstName,
            //        opt => opt.MapFrom(src => src.FirstName)
            //    )
            //    .ForMember(d => d.LastName,
            //        opt => opt.MapFrom(src => src.LastName)
            //    )
            //    .ForMember(d => d.ProvinceName,
            //        opt => opt.MapFrom(src => src.City.Province.ProvinceName)
            //    )
            //    .ForMember(d => d.CityName,
            //        opt => opt.MapFrom(src => src.City.CityName)
            //    ).ReverseMap();

            //We can map both directions with ReverseMap() method, including unflattening:



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


            //CreateMap<Customer.DataLayer.Customer, CustomerViewModel>()
            //    .ForMember(d => d.FirstName,
            //        opt => opt.MapFrom(src => src.FirstName)
            //    )
            //    .ForMember(d => d.LastName,
            //        opt => opt.MapFrom(src => src.LastName)
            //    )
            //    .ForMember(d => d.ProvinceName,
            //        opt => opt.MapFrom(src => src.City.Province.ProvinceName)
            //    )
            //    .ForMember(d => d.CityName,
            //        opt => opt.MapFrom(src => src.City.CityName)
            //    );

            //CreateMap<CustomerViewModel, Customer.DataLayer.Customer>()
            //    .ForMember(d => d.FirstName,
            //        opt => opt.MapFrom(src => src.FirstName)
            //    )
            //    .ForMember(d => d.LastName,
            //        opt => opt.MapFrom(src => src.LastName)
            //    )
            //    .ForPath(d => d.CityID,
            //        opt => opt.MapFrom(src => FindCity(src.CityName))
            //    )
            //    .ForPath(d => d.City.ProvinceID,
            //        opt => opt.MapFrom(src => FindCity(src.CityName)));


        }

        private int FindCity(string cityName)
        {
            return 1;
        }
    }
}