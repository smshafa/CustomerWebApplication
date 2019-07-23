﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Customer.ServiceLayer.ViewModels
{
    public class CustomerViewModel : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (IsNumeric(FirstName))
            {
                errors.Add(new ValidationResult("نام باید حروف باشد."));
            }

            return errors;
        }



        private bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }


    }
}