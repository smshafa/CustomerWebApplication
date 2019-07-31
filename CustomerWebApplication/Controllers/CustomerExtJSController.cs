using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Customer.ServiceLayer.ViewModels;
using Newtonsoft.Json;

namespace CustomerWebApplication.Controllers
{
    public class CustomerExtJSController : Controller
    {
        // GET: CustomerExtJS
        public ActionResult Index()
        {
            return View();
        }


        public Task<HttpResponseMessage> CallGetIDApi()
        {
            // Arrange
            Task<HttpResponseMessage> response;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44311/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // New code:
            response = client.GetAsync("api/customers/");

            return response;
        }


        public JsonResult Load()
        {

            Helper.CustomerWebApiRepository customerWebApiRepository = new Helper.CustomerWebApiRepository();


            Task<HttpResponseMessage> response = customerWebApiRepository.Get();
            response.Wait();

            string jsonContent = default;

            List<CustomerViewModel> customerViewModel = null;
            if (response.Result.IsSuccessStatusCode)
            {
                HttpContent requestContent = response.Result.Content;
                jsonContent = requestContent.ReadAsStringAsync().Result;

                customerViewModel = JsonConvert.DeserializeObject<List<CustomerViewModel>>(jsonContent);

                return Json(new
                {
                    data = customerViewModel,
                    success = true,
                    message = "Loaded data is successful."
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                data = customerViewModel,
                success = false,
                message = "Loaded data was failed."
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(string data)
        {
            //insert Create code
            //return Json(new
            //{
            //    data = new Contact(data[0].Name, data[0].Phone, data[0].Email, data[0].BirthDate, data[0].IsMarried, data[0].NoOfCar),
            //    success = true,
            //    message = "Create method called successfully"
            //});

            Helper.CustomerWebApiRepository customerWebApiRepository = new Helper.CustomerWebApiRepository();
            CustomerViewModel customerViewModel = JsonConvert.DeserializeObject<CustomerViewModel>(data);


            Task<HttpResponseMessage> response = customerWebApiRepository.Post(customerViewModel);
            response.Wait();

            if (response.Result.StatusCode != HttpStatusCode.Created)
                return Json(new
                {
                    success = true,
                    message = "Create method called unsuccessfully"
                });
            else
                return Json(new
                {
                    success = true,
                    message = "Create method called successfully"
                });
        }

        [HttpPost]
        public JsonResult Update(string data)
        {
            //Console.WriteLine(data[0].BirthDate);
            //insert Update code
            Helper.CustomerWebApiRepository customerWebApiRepository = new Helper.CustomerWebApiRepository();
            CustomerViewModel customerViewModel = JsonConvert.DeserializeObject<CustomerViewModel>(data);


            Task<HttpResponseMessage> response = customerWebApiRepository.Put(customerViewModel.CustomerID, customerViewModel);
            response.Wait();

            if (response.Result.StatusCode != HttpStatusCode.OK)
                return Json(new
                {
                    success = false,
                    message = "Update method called unsuccessfully"
                });
            else

                return Json(new
                {
                    success = true,
                    message = "Update method called successfully"
                });

            return Json(new
            {
                success = true,
                message = "Update method called successfully"
            });
        }

        [HttpPost]
        public JsonResult Delete(string data)
        {
            Helper.CustomerWebApiRepository customerWebApiRepository = new Helper.CustomerWebApiRepository();
            CustomerViewModel customerViewModel = JsonConvert.DeserializeObject<CustomerViewModel>(data);


            Task<HttpResponseMessage> response = customerWebApiRepository.Delete(customerViewModel.CustomerID);
            response.Wait();

            if (response.Result.StatusCode != HttpStatusCode.OK)
                return Json(new
                {
                    success = false,
                    message = "Delete method called unsuccessfully"
                });
            else

                return Json(new
                {
                    success = true,
                    message = "Delete method called successfully"
                });
        }
    }
}
