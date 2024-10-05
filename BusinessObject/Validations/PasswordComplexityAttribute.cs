using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class PasswordComplexityAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;
        if (password == null)
        {
            return new ValidationResult("Password is required.");
        }

        // Kiểm tra độ dài tối thiểu
        if (password.Length < 8)
        {
            return new ValidationResult("Password must be at least 8 characters long.");
        }

        // Kiểm tra có ít nhất một ký tự hoa
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return new ValidationResult("Password must contain at least one uppercase letter.");
        }

        // Kiểm tra có ít nhất một chữ số
        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            return new ValidationResult("Password must contain at least one digit.");
        }

        // Kiểm tra có ít nhất một ký tự đặc biệt
        if (!Regex.IsMatch(password, @"[\W_]"))
        {
            return new ValidationResult("Password must contain at least one special character.");
        }

        return ValidationResult.Success;
    }
}
