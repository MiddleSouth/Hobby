using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RLSimulation.CustomValidation
{
    public class RangeNotEqualValidationAttribute : ValidationAttribute
    {
        public object Maximum { get; set; }

        public object Minumum { get; set; }

        /// <summary>
        /// 引数なしコンストラクタを禁止する
        /// </summary>
        private RangeNotEqualValidationAttribute() { }

        /// <summary>
        /// int型による値のチェック
        /// </summary>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        public RangeNotEqualValidationAttribute(int min, int max)
        {
            Minumum = min;
            Maximum = max;
        }

        /// <summary>
        /// double型による値のチェック
        /// </summary>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        public RangeNotEqualValidationAttribute(double min, double max)
        {
            Minumum = min;
            Maximum = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double num;
            double min;
            double max;

            if (double.TryParse(value.ToString(), out num)
                && double.TryParse(Minumum.ToString(), out min)
                && double.TryParse(Maximum.ToString(), out max))
            {
                if (min < num && num < max)
                {
                    return ValidationResult.Success;
                }
            }

            if(string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = string.Format("{0} < x < {1} の範囲で入力してください", Minumum, Maximum);
            }

            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}
