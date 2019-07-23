using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Customer.ServiceLayer.ViewModels
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "خالی نمی‌تواند باشد.")]
        [MaxLength(40, ErrorMessage = "حداکثر 40 حرف")]
        [Display(Name = "نام")]
        public string FirstName { set; get; }
        [Required(ErrorMessage = "خالی نمی‌تواند باشد.")]
        [MaxLength(40, ErrorMessage = "حداکثر 40 حرف")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { set; get; }
        [Display(Name = "نام استان")]
        public string ProvinceName { set; get; }
        [Display(Name = "نام شهر")]
        public string CityName { set; get; }
    }
}