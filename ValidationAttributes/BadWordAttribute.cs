using System;
using System.ComponentModel.DataAnnotations;

namespace HoangDuonglab5.ValidationAttributes
{
    // Kiểm tra không chứa các từ nhạy cảm
    public class BadWordAttribute : ValidationAttribute
    {
        private readonly string[] _badWords;

        public BadWordAttribute(params string[] badWords)
        {
            _badWords = badWords;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string text = value.ToString().ToLower();
                foreach (var word in _badWords)
                {
                    if (text.Contains(word.ToLower()))
                    {
                        return new ValidationResult($"Mô tả không được chứa từ nhạy cảm: '{word}'");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}