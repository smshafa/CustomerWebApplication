using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Customer.BusinessLayer;
using Customer.DataLayer;
using Customer.ServiceLayer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestCustomerProject
{
    [TestClass]
    public class UnitTest_BL
    {

        public UnitTest_BL()
        {
            // AutoMapper Configuration
            AutoMapperConfiguration.Configure();
        }


        // use this post method from browser to test post method:
        //$.ajax(
        //{
        //    url: 'https://localhost:44311/api/customers',
        //    type: 'POST',
        //    contentType: 'application/json',
        //    data: JSON.stringify({
        //        "FirstName": "haha",
        //        "LastName": "ddd",
        //        "ProvinceName": "Hamedan",
        //        "CityName": "Hamedan"
        //    })
        //})


        // Put/ Update
        //$.ajax(
        //{
        //    url: 'https://localhost:44311/api/customers/14',
        //    type: 'PUT',
        //    contentType: 'application/json',
        //    data: JSON.stringify(

        //    {
        //        "FirstName": "update",
        //        "LastName": "update",
        //        "ProvinceName": "Hamedan",
        //        "CityName": "Hamedan"

        //    }
        //    )
        //})

        [TestMethod]
        public void Insert()
        {
            // Arrange:
            // Web API posts CustomerViewModel via json format and will be deserialized into CustomerViewModel
            CustomerViewModel customer = new CustomerViewModel();
            customer.FirstName = "nameTest";
            customer.LastName = "familyTest";
            customer.CityName = "Hamedan";
            customer.ProvinceName = "Hamedan";

            // Creating an unit of work.
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();

            // Mapping ViewModel to Model to provide database format model
            Customer.DataLayer.Customer entityCustomer = Mapper.Map<CustomerViewModel, Customer.DataLayer.Customer>(customer);

            // Find city and province entity
            City city = unitOfWork.GetRepoInstance<City>().GetAll().Where(s => s.CityName == customer.CityName).Single();
            Province province = unitOfWork.GetRepoInstance<Province>().GetAll().Where(s => s.ProvinceName == customer.ProvinceName).Single();
            city.Province = province;

            // change city becuase city is a new city in mapping and we don't want to create new city.
            entityCustomer.City = city;

            // Creating a customer item.
            //Customer.DataLayer.Customer c = new Customer.DataLayer.Customer()
            //{
            //    FirstName = "test",
            //    LastName = "test",
            //    City = city,
            //};

            GenericRepository<Customer.DataLayer.Customer> repCustomer =
                unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>();

            int expctedCount = repCustomer.Count();

            repCustomer.Insert(entityCustomer);
            repCustomer.Save();

            Assert.AreEqual(++expctedCount, repCustomer.Count());
        }

        [TestMethod]
        public void Delete()
        {
            int customerID = 16;
            // Creating an unit of word cause to use one dbcontext.
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();

            GenericRepository<Customer.DataLayer.Customer> repCustomer =
                    unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>();
            int expctedCount = repCustomer.Count();

            Customer.DataLayer.Customer customer = unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetAll().Where(c => c.CustomerID == customerID).Single();
            unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().Delete(customer);
            unitOfWork.SaveChanges();

            Assert.AreEqual(--expctedCount, repCustomer.Count());
        }

        [TestMethod]
        public void Update()
        {
            int id = 14;
            CustomerViewModel customer = new CustomerViewModel();
            customer.FirstName = "nameTestUpdate";
            customer.LastName = "familyTestUpdate";
            customer.CityName = "Tehran";
            customer.ProvinceName = "Tehran";


            // Creating an unit of word cause to use one dbcontext.
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();

            // Get customer
            Customer.DataLayer.Customer customerSingle
                = unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetAll().Where(c => c.CustomerID == id).Single();

            // Find city and province entity
            City city = unitOfWork.GetRepoInstance<City>().GetAll().Where(s => s.CityName == customer.CityName).Single();
            Province province = unitOfWork.GetRepoInstance<Province>().GetAll().Where(s => s.ProvinceName == customer.ProvinceName).Single();
            city.Province = province;


            customerSingle.FirstName = customer.FirstName;
            customerSingle.LastName = customer.LastName;
            customerSingle.City = city;


            // Get a repository of customer to do CRUD.
            GenericRepository<Customer.DataLayer.Customer> repCustomer =
                unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>();


            // Inserting new customer data
            repCustomer.Update(customerSingle);
            repCustomer.Save();


            // Get customer
            customerSingle
                = unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetAll().Where(c => c.CustomerID == id)
                    .Single();
            Assert.AreEqual(customer.FirstName, customerSingle.FirstName);
        }
    }
}
