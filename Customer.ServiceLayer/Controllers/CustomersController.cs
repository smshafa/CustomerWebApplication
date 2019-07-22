using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Customer.BusinessLayer;
using CustomerWebApplication.Models;
using CustomerWebApplication.ViewModel;

namespace Customer.ServiceLayer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomersController : ApiController
    {
        // GET api/values
        public IEnumerable<CustomerViewModel> Get()
        {
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            IEnumerable<Customer.DataLayer.Customer> customers = unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetAll();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Customer.DataLayer.Customer, CustomerViewModel>()
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
                    );
            });

            Mapper.AssertConfigurationIsValid();

            // Act
            var result = Mapper.Map<IEnumerable<Customer.DataLayer.Customer>, IEnumerable<CustomerViewModel>>(customers);

            return result;
        }

        
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
