using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customer.ServiceLayer.Models
{
    public class CustomerModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "خالی نمی‌تواند باشد.")]
        public int CustomerID { set; get; }

        public ProvinceModel ProvinceName { set; get; }

        public CityModel CityName { set; get; }

        [Required(ErrorMessage = "خالی نمی‌تواند باشد.")]
        [MaxLength(40, ErrorMessage = "حداکثر 40 حرف")]
        [Display(Name = "نام")]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "خالی نمی‌تواند باشد.")]
        [MaxLength(40, ErrorMessage = "حداکثر 40 حرف")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { set; get; }
    }
}