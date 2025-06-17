using System;
using System.ComponentModel.DataAnnotations;

namespace CakeApp.Models // Or a new namespace like CakeApp.ValidationAttributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // If the date is not required, null is valid.
                // If it *is* required, [Required] attribute should be used too.
                return ValidationResult.Success;
            }

            if (value is DateTime dateTime)
            {
                // Compare the date part only to ignore time differences for "today"
                if (dateTime.Date > DateTime.Today)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? "The date must be in the future.");
                }
            }

            // This should not happen if applied to DateTime properties
            return new ValidationResult("Invalid date format.");
        }
    }
}