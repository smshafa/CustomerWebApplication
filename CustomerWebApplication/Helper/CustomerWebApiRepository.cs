using Customer.ServiceLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace CustomerWebApplication.Helper
{
    /// <summary>
    /// This class handle all of customer web API communications such as HTTP verbs.
    /// </summary>
    public class CustomerWebApiRepository
    {

        public Task<HttpResponseMessage> Get()
        {
            // Arrange
            Task<HttpResponseMessage> response;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44311/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            response = client.GetAsync($"api/customers/");

            return response;
        }


        public Task<HttpResponseMessage> Get(int customerID)
        {
            // Arrange
            Task<HttpResponseMessage> response;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44311/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            response = client.GetAsync($"api/customers/{customerID}");
                        
            return response;
        }


        public Task<HttpResponseMessage> Post(CustomerViewModel customer)
        {          
            Task<HttpResponseMessage> response = default;
            HttpClient client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri("https://localhost:44311/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
              
                var json = new JavaScriptSerializer().Serialize(customer);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
                response = client.PostAsync("api/customers", content);
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return response;
        }


        public Task<HttpResponseMessage> Put(int customerID, CustomerViewModel customer)
        {            
            Task<HttpResponseMessage> response = default;
            HttpClient client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri("https://localhost:44311/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
           
                var json = new JavaScriptSerializer().Serialize(customer);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
                response = client.PutAsync($"api/customers/{customerID}", content);
            }
            catch (Exception ex)
            {
                throw;
            }
         
            return response;
        }


        public Task<HttpResponseMessage> Delete(int customerID)
        {
            // Arrange
            Task<HttpResponseMessage> response = default;            

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44311/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            response = client.DeleteAsync($"api/customers/{customerID}");

            return response;
        }
    }
}