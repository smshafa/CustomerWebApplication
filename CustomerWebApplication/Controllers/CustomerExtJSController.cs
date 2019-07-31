using System;
using System.Collections.Generic;
using System.Linq;
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
        

        public JsonResult Load(int? start, int? limit)
        {

            Task<HttpResponseMessage> response = CallGetIDApi();
            response.Wait();


            string jsonContent = default;

            List<CustomerViewModel> customerViewModel = null;
            if (response.Result.IsSuccessStatusCode)
            {
                HttpContent requestContent = response.Result.Content;
                jsonContent = requestContent.ReadAsStringAsync().Result;
                
                customerViewModel = JsonConvert.DeserializeObject<List<CustomerViewModel>>(jsonContent);               
            }           

            return Json(new
            {
                data = customerViewModel,
                success = true,
                message = "Loaded data"
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
            return Json(new
            {

            });
        }

        [HttpPost]
        public JsonResult Update(string data)
        {
            //Console.WriteLine(data[0].BirthDate);
            //insert Update code


            return Json(new
            {
                success = true,
                message = "Update method called successfully"
            });
        }

        [HttpPost]
        public JsonResult Delete(string data)
        {
            //insert Delete code
            return Json(new
            {
                success = true,
                message = "Delete method called successfully"
            });
        }
    }
}
