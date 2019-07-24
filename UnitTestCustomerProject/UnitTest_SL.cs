using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Customer.ServiceLayer.ViewModels;
//using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http.Results;

namespace UnitTestCustomerProject
{

    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        //public void Configuration(IAppBuilder appBuilder)
        //{
        //    // Configure Web API for self-host. 
        //    HttpConfiguration config = new HttpConfiguration();
        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );

        //    appBuilder.Use(config);
        //}
    }

    [TestClass]
    public class UnitTest_SL
    {

        public Task<HttpResponseMessage> CallGetIDApi()
        {
            // Arrange
            Task<HttpResponseMessage> response;

            //string baseAddress = "http://localhost:9000/";
            // Start OWIN host 
            //using (WebApp.Start<Startup>(url: baseAddress))
            //{

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44311/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                response = client.GetAsync("api/customers/1");
                response.Wait();
            }


            //}
            return response;
        }

        [TestMethod]
        public void TestGetApi()
        {
            Task<HttpResponseMessage> response = CallGetIDApi();
            if (response.Result.IsSuccessStatusCode)
            {
                HttpContent requestContent = response.Result.Content;
                string jsonContent = requestContent.ReadAsStringAsync().Result;
                //CONTACT contact = JsonConvert.DeserializeObject<CONTACT>(jsonContent);

                Assert.AreEqual("\"value\"", jsonContent);
                //customer product = await response.Content.ReadAsStringAsync().Result > Product > ();
                //Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
            }
        }



        //https
        public Task<HttpResponseMessage> CallPostApi()
        {
            // Arrange
            CustomerViewModel customer = new CustomerViewModel
            {
                FirstName = "nameTestAPI",
                LastName = "familyTestAPI",
                CityName = "Hamedan",
                ProvinceName = "Hamedan"
            };


            Task<HttpResponseMessage> response = default;

            HttpClient client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri("https://localhost:44311/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                

                StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                //var data = new StringContent(JsonConvert.SerializeObject(customer));

                response = client.PostAsJsonAsync("api/customers", content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            //response.Wait();
            return response;
        }

        [TestMethod]
        public void TestPostApi()
        {
            Task<HttpResponseMessage> response = CallPostApi();
            response.Wait();
            
            if (response.Result.StatusCode != HttpStatusCode.Created)
                Assert.Fail();


        }
    }
}

