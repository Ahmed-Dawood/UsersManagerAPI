using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class ResetPasswordInfo
    {
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(255, ErrorMessage = "Max 24 character")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 character", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 character", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
