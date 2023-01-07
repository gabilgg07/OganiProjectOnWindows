using System;
using System.ComponentModel.DataAnnotations;
using Ogani.WebUI.AppCode.Extensions;

namespace Ogani.WebUI.AppCode.DataAnnotation
{
	public class RequiredEmailAttribute : RequiredAttribute
	{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // is-in bu versiyasi tryParse-a oxsayir 
            if (value is string email && email.IsEmail())
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("E-poct unvaniniz uygun deyil");
        }
    }
}

