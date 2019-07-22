using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CustomerWebApplication.Models
{
    public class CityModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CityID { set; get; }
        [Display(Name = "نام شهر")]
        public string CityName { set; get; }
    }
}