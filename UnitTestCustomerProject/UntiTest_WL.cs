using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Customer.BusinessLayer;
using Customer.ServiceLayer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestCustomerProject
{
    [TestClass]
    public class UntiTest_WL
    {
        [TestMethod]
        public void TestMapping()
        {
            // Arrange
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            IEnumerable<Customer.DataLayer.Customer> customers = unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetAll();



            IEnumerable<CustomerViewModel> customerViewModel = new List<CustomerViewModel>();


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

            //Assert
            Assert.AreNotEqual(result.Count(), 0);
        }
    }
}
