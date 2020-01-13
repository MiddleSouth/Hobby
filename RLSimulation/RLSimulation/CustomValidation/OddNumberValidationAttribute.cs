using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RLSimulation.CustomValidation
{
    public class OddNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int num;

            if(int.TryParse(value.ToString(), out num))
            {
                if(num % 2 == 1)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("奇数を入力してください。", new[] {  validationContext.MemberName });
        }
    }
}
