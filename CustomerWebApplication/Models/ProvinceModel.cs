using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerWebApplication.Models
{
    public class ProvinceModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ProvinceID { set; get; }
        [Display(Name = "نام استان")]
        public string ProvinceName { set; get; }
    }
}