using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;

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
        
        public Task<HttpResponseMessage> CallApi()
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
        public void TestApi()
        {
            Task<HttpResponseMessage> response = CallApi();
            if (response.Result.IsSuccessStatusCode)
            {
                Assert.AreEqual("value", response.Result.Content);
                //customer product = await response.Content.ReadAsStringAsync().Result > Product > ();
                //Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
            }
        }
    }
}
