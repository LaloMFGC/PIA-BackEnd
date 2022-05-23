using System.ComponentModel.DataAnnotations;

namespace PIAWebApi.Validations
{
    public class ValidationPhoneAttribute: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if(value == null || string.IsNullOrEmpty(value.ToString())) {
                return new ValidationResult($"El campo Phone esta vacio");
            }

            var data = value.ToString();

            bool valid = true;

            for (int i = 0; i < data.Length; i++) {

                if ((data[i] < '0' ) || (data[i] > '9') )
                {
                    valid = false;
                }

            }

            if(!valid) {
                return new ValidationResult("El campo Phone tiene caracteres que no son numericos");
            }
            else
            {
                return ValidationResult.Success;
            }

            
        }


    }
}
