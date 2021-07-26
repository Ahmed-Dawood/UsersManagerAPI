using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class RegisterInfo : LoginInfo
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 16 character", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }
    }
}
