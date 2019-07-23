using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer.ServiceLayer.ViewModels
{
    public class CustomerViewModel
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string ProvinceName { set; get; }
        public string CityName { set; get; }
    }
}