using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class RegisterInfo : LoginInfo
    {
        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 character", MinimumLength = 5)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 character", MinimumLength = 5)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
