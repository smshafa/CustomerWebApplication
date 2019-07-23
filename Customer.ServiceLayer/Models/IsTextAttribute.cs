using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Customer.ServiceLayer.ViewModels
{
    public class IsTextAttribute : ValidationAttribute, IClientValidatable
    {

        public IsTextAttribute()
        {
            ErrorMessage = "لطفا مقدار وارد کنید.";
        }

        public override bool IsValid(object value)
        {
            return !string.IsNullOrEmpty((string) value);

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
            ControllerContext context)
        {
            return new List<ModelClientValidationRule>
            {
                new ModelClientValidationRule
                {
                    ValidationType = "required",
                    ErrorMessage = "لطفا از حروف استفاده کنید."
                }

            };
        }
    }
}

