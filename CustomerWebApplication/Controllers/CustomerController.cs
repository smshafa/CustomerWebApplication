using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Customer.BusinessLayer;
using CustomerWebApplication.ViewModel;

namespace CustomerWebApplication.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
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
                        opt => opt.MapFrom(src => src.Province.ProvinceName)
                    )
                    .ForMember(d => d.CityName,
                        opt => opt.MapFrom(src => src.Province.City.CityName)
                    );
            });

            Mapper.AssertConfigurationIsValid();

            // Act
            var result = Mapper.Map<IEnumerable<Customer.DataLayer.Customer>, IEnumerable<CustomerViewModel>>(customers);

            return View(result);
        }
    }
}