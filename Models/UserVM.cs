using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace ChrisHaniHospital.Models
{
    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required.");
            }

            // Regular expression to check for at least one number and one capital letter
            var regex = new Regex(@"^(?=.*[A-Z])(?=.*\d).+$");
            if (!regex.IsMatch(password))
            {
                return new ValidationResult(
                    "Password must contain at least one number and one capital letter."
                );
            }

            return ValidationResult.Success;
        }
    }

    public class UserVM
    {
        [Key]
        public int Id { get; set; }
        public string? MainUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StrongPassword]
        public string? Password { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Registration_Number { get; set; }

        [Required]
        public string Speciality { get; set; }
    }
}
