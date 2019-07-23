﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer.ServiceLayer.Models;

namespace Customer.ServiceLayer.ViewModels
{
    public class CustomerListViewModel
    {
        public List<CustomerModel> CustomersList { set; get; }
        public List<ProvinceModel> ProvincesList { get; set; }
        public List<CityModel> CityList { get; set; }
    } 
}