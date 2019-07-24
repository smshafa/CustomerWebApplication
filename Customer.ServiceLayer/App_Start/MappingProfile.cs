using AutoMapper;
using Customer.ServiceLayer.ViewModels;

namespace Customer.ServiceLayer.App_Start
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



            CreateMap<DataLayer.Customer, CustomerViewModel>()
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
                ).ReverseMap();

            //We can map both directions with ReverseMap() method, including unflattening:

        }
    }
}