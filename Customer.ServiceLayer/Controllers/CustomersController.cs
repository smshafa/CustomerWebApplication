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
        public string Get(int id)
        {
            return "value";
        }
    }
}
