using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using Customer.BusinessLayer;
using Customer.ServiceLayer.ViewModels;

namespace CustomerWebApplication.Controllers
{
    public class UpdateCustomerController : Controller
    {
        // GET: UpdateCustomer
        [HttpGet]
        public ActionResult CusForm(int ID = 1)
        {

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            Customer.DataLayer.Customer customers =
                unitOfWork.GetRepoInstance<Customer.DataLayer.Customer>().GetById(ID);

            IEnumerable<CustomerViewModel> customerViewModel = new List<CustomerViewModel>();

            CustomerViewModel result = Mapper.Map<Customer.DataLayer.Customer, CustomerViewModel>(customers);

            ViewBag.ProvinceList = ToSelectListProvince();
            //ViewBag.CityList = ToSelectListCity();
            result.CityList = GetCityList();
            result.CustomerID = ID;
            TempData["CusID"] = ID;
            return View(result);
        }

        [HttpPost]
        public ActionResult CusForm(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                //redirect
            }

            int customerID = Convert.ToInt32(TempData["CusID"]);

            Task<HttpResponseMessage> response = default;

            HttpClient client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri("https://localhost:44311/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));


                //StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var json = new JavaScriptSerializer().Serialize(customerViewModel);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");


                //var data = new StringContent(JsonConvert.SerializeObject(customer));
                response = client.PutAsync($"api/customers/{customerID}", content);
                response.Wait();
                if (response.Result.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("updated");
                }
                else
                {
                    Console.WriteLine("was not updated");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            customerViewModel.CityList = GetCityList();
            ViewBag.ProvinceList = ToSelectListProvince();
            //ViewBag.CityList = ToSelectListCity();
            
            return View(customerViewModel);
        }
        [NonAction]
        public SelectList ToSelectListProvince()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            var provinces = unitOfWork.GetRepoInstance<Customer.DataLayer.Province>().GetAll();
            var q = from c in provinces
                select new {c.ProvinceID, c.ProvinceName};

            foreach (var p in q)
            {
                list.Add(new SelectListItem()
                {
                    Text = p.ProvinceName,
                    //Value = p.ProvinceID.ToString()
                    Value = p.ProvinceName
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        [NonAction]
        public SelectList ToSelectListCity()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            var cities = unitOfWork.GetRepoInstance<Customer.DataLayer.City>().GetAll();
            var q = from c in cities
                select new {c.CityID, c.CityName};

            foreach (var p in q)
            {
                list.Add(new SelectListItem()
                {
                    Text = p.CityName,
                    Value = p.CityName
                    //Value = p.CityID.ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        // Populate Building values to DropDownList
        private IEnumerable<SelectListItem> GetCityList()
        {
            CustomerUnitOfWork unitOfWork = new CustomerUnitOfWork();
            var cities = unitOfWork.GetRepoInstance<Customer.DataLayer.City>().GetAll();

            var cityList = cities.Select(c => new SelectListItem
            {
                Value = c.CityName,
                Text = c.CityName
            });

            return new SelectList(cityList, "Value", "Text");
            //return (bldg);
        }

        
    }
}

