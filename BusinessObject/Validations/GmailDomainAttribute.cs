using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Validations
{

    public class GmailDomainAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string? email = value.ToString();
                if (email.EndsWith("@gmail.com"))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Email must end with @gmail.com.");
                }
            }
            return new ValidationResult("Email is required.");
        }
    }
}
