using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTemplate.Attributes
{
    public class RegistrationTokenAttribute : ValidationAttribute
    {
        private const string Token = "xyz";
        private const string RegistrationTokenErrorMessage = "Registration Token is invalid.";


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return value.Equals(Token) ? ValidationResult.Success : new ValidationResult(ErrorMessage ?? RegistrationTokenErrorMessage);
        }

        // Needed for client-side validation (implements IClientModelValidator)
        //public void AddValidation(ClientModelValidationContext context)
        //{
        //    if(context == null)
        //    {
        //        throw new NullReferenceException(nameof(ClientModelValidationContext));
        //    }

        //    context.Attributes.Add("data-val", "true");
        //    context.Attributes.Add("data-val-regtoken", ErrorMessage ?? RegistrationTokenErrorMessage);
        //    context.Attributes.Add("data-val-regtoken-val", _expectedToken.ToString());
        //}
    }
}
