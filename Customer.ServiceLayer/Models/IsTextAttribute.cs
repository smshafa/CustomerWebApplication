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
        private const string VALIDATION_TYPE = "customemail";
        private const string EMAIL_REGEX = @"put your regex here";

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
                    ErrorMessage = "خالی نباشد"
                }
                ,
                new ModelClientValidationRule
                {
                    ValidationType = VALIDATION_TYPE,
                    ErrorMessage = ":)"
                }

            };
        }
    }
}

