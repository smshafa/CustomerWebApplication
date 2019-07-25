using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Customer.BusinessLayer;
using Customer.ServiceLayer.ViewModels;

namespace CustomerWebApplication.Controllers
{
    public class CustomerController : Controller
    {
        private IEnumerable<CustomerViewModel> getCustomerViewModel()
        {
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            IEnumerable<Customer.DataLayer.Customer> customers = unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetAll();

            // Mapping 
            var result = Mapper.Map<IEnumerable<Customer.DataLayer.Customer>, IEnumerable<CustomerViewModel>>(customers);
            return result;
        }

        // GET: Customer
        public ActionResult Index()
        {
            var result = getCustomerViewModel();

            return View(result);
        }

        public ActionResult Create()
        {

            return View();
        }

        public ViewResult Delete(int? id)
        {
            if (id == null)
                return View();
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44311/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // New code:
            HttpResponseMessage response = client.DeleteAsync($"api/customers/{id}").Result;

            if (response.StatusCode != HttpStatusCode.OK)
                Console.WriteLine("deleted");

            return View("Index", getCustomerViewModel());
        }
    }
}