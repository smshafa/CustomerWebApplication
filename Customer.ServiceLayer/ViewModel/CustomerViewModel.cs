using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerWebApplication.Models;

namespace Customer.ServiceLayer.ViewModel
{
    public class CustomerViewModel
    {
        public List<CustomerModel> CustomersList { set; get; }
        public List<ProvinceModel> ProvincesList { get; set; }
        public List<CityModel> CityList { get; set; }
    } 
}
