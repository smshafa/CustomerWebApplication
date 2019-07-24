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
using Customer.ServiceLayer.ViewModels;
using Customer.DataLayer;

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

            IEnumerable<CustomerViewModel> customerViewModel = new List<CustomerViewModel>();

            var result = Mapper.Map<IEnumerable<Customer.DataLayer.Customer>, IEnumerable<CustomerViewModel>>(customers);

            return result;
        }

        
        // GET api/values/5
        public CustomerViewModel Get(int id)
        {
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            Customer.DataLayer.Customer customers =
                unitOfWork.GetRepoInstance<DataLayer.Customer>().GetById(id);

            IEnumerable<CustomerViewModel> customerViewModel = new List<CustomerViewModel>();

            var result = Mapper.Map<Customer.DataLayer.Customer, CustomerViewModel>(customers);

            return result;
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]CustomerViewModel customer)
        {
            // There is no parameter or it is not passed corrected.
            if (customer == null)
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);

            // Creating an unit of word cause to use one dbcontext.
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            // Mapping ViewModel into Model
            // ** In mapping ViewModel into Model, a new city will be created and we should aware of it.
            DataLayer.Customer entityCustomer = Mapper.Map<CustomerViewModel, DataLayer.Customer>(customer);
            // Get a repository of customer to do CRUD.
            GenericRepository < DataLayer.Customer > repCustomer =
                    unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>();


            // Find city and province entity
            City city = unitOfWork.GetRepoInstance<City>().GetAll().Where(s => s.CityName == customer.CityName).Single();
            Province province = unitOfWork.GetRepoInstance<Province>().GetAll().Where(s => s.ProvinceName == customer.ProvinceName).Single();
            city.Province = province;

            // assign a city that is existed in database, because city is a new city in mapping and we don't want to create new a city.
            entityCustomer.City = city;

            // Inserting new customer data
            repCustomer.Insert(entityCustomer);
            repCustomer.Save();

            // Response to caller
            var response = Request.CreateResponse<DataLayer.Customer>(System.Net.HttpStatusCode.Created, entityCustomer);

            return response;
        }

        // PUT api/values/5
        public HttpResponseMessage Put(int id, [FromBody]CustomerViewModel customer)
        {
            if (id < 1 || customer == null)
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);

            // Creating an unit of word cause to use one dbcontext.
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();

            // Get customer
            Customer.DataLayer.Customer customerSingle
                = unitOfWork.GetRepoInstance<DataLayer.Customer>().GetAll().Where(c => c.CustomerID == id).Single();

            // Find city and province entity
            City city = unitOfWork.GetRepoInstance<City>().GetAll().Where(s => s.CityName == customer.CityName).Single();
            Province province = unitOfWork.GetRepoInstance<Province>().GetAll().Where(s => s.ProvinceName == customer.ProvinceName).Single();
            city.Province = province;


            customerSingle.FirstName = customer.FirstName;
            customerSingle.LastName = customer.LastName;
            customerSingle.City = city;

            
            // Get a repository of customer to do CRUD.
            GenericRepository<DataLayer.Customer> repCustomer =
                unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>();


            // Inserting new customer data
            repCustomer.Update(customerSingle);
            repCustomer.Save();

            // Response to caller
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            if(id < 1)
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);

            // Creating an unit of word cause to use one dbcontext.
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();


            Customer.DataLayer.Customer customer = unitOfWork.GetRepoInstance<DataLayer.Customer>().GetAll().Where(c => c.CustomerID == id).Single();
            unitOfWork.GetRepoInstance<DataLayer.Customer>().Delete(customer);
            unitOfWork.SaveChanges();

            // Response to caller
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}
