using System.ComponentModel.DataAnnotations;

namespace HoangDuonglab5.ValidationAttributes
{
    public class SalePriceAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public SalePriceAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (float?)value;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Thuộc tính {_comparisonProperty} không tồn tại.");
            }

            var priceValue = (float?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && priceValue.HasValue)
            {
                if (currentValue < 0)
                {
                    return new ValidationResult("Giá khuyến mãi không được nhỏ hơn 0");
                }

                if (currentValue > priceValue * 0.9f)
                {
                    return new ValidationResult("Giá khuyến mãi phải nhỏ hơn giá chuẩn ít nhất 10%");
                }
            }

            return ValidationResult.Success;
        }
    }
}