using System.ComponentModel.DataAnnotations;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class LoginInfo
    {
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(255, ErrorMessage = "Max 24 character")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 character", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
