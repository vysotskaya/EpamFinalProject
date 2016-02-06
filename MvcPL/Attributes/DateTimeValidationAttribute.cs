using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Attributes
{
    public class DateTimeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date >= DateTime.Now)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Incorrect block date.");
        }
    }
}