using BusinessObject.Validations;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public int Type { get; set; }
        public virtual ICollection<Rental>? Rentals { get; set; }


        [GmailDomain]
        public string? Email { get; set; }
        [PasswordComplexity]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public string GetContactInfo()
        {
            return $"{LastName},{FirstName}, {Email}, {PhoneNumber}";
        }
    }
}
