using System.ComponentModel.DataAnnotations;

namespace Pri.Ee.Api.CustomValidator
{
    public static class CustomValidators
    {
        public static ValidationResult ValidateAge(DateTime birthDate, ValidationContext context)
        {
            var age = DateTime.Now.Year - birthDate.Year;
            if (birthDate > DateTime.Now.AddYears(-age)) //Checken of de verjaardag al gevallen is
            {
                age--; //zoniet doen we hier min 1
            }

            if (age < 16)
            {
                return new ValidationResult("You must be at least 16 years old to register.");
            }

            return ValidationResult.Success;
        }
    }
}
