using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Customer.BusinessLayer;
using Customer.ServiceLayer.ViewModels;

namespace CustomerWebApplication.Controllers
{
    public class NewCustomerController : Controller
    {
        [HttpGet]
        // GET: NewCustomer
        public ActionResult CusForm()
        {

            ViewBag.CityList = ToSelectList();
            return View();
        }


        //https://www.c-sharpcorner.com/article/dropdownlist-in-asp-net-mvc/
        [NonAction]
        public SelectList ToSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            var provinces = unitOfWork.GetRepoInstance<Customer.DataLayer.Province>().GetAll();
            var q = from c in provinces
                    select new { c.ProvinceID, c.ProvinceName };

            foreach(var p in q)
            {
                list.Add(new SelectListItem()
                {
                    Text = p.ProvinceName,
                    Value = p.ProvinceID.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [HttpPost]
        public ActionResult CusForm(CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid == true)
            {
                string name = customerViewModel.FirstName;
                string porvince = customerViewModel.ProvinceName;
            }

            HttpClient client = new HttpClient();


            client.BaseAddress = new Uri("https://localhost:44311/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            
            var json = new JavaScriptSerializer().Serialize(customerViewModel);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("api/customers", content).Result;

            if (response.StatusCode == HttpStatusCode.Created)
                Console.WriteLine("inserted");

            // very important when we want to be in view after post.
            ViewBag.CityList = ToSelectList();
            return View();
        }

        public ActionResult SelectCategory()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Action", Value = "0" });

            items.Add(new SelectListItem { Text = "Drama", Value = "1" });

            items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

            items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

            ViewBag.MovieType = items;

            return View();

        }


    }
}