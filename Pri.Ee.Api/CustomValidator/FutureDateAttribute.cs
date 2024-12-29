using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Api.CustomValidator
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date < DateTime.Now)
                {
                    return new ValidationResult("The target date cannot be in the past.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
