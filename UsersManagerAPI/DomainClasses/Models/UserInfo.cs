using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class UserInfo : IUserInfo
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 character", MinimumLength = 5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 character", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Password { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(50, ErrorMessage = "Max 50 character")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "Max 50 character")]
        public string LastName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required]
        [Column(TypeName = "varchar(64)")]
        public string HashPassword { get; set; }

        [Required]
        [Column(TypeName = "varchar(48)")]
        public string SaltKey { get; set; }

        [Required]
        public bool IsMailConfirmed { get; set; }

        [MaxLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string Role { get; set; }

        [Required]
        [MaxLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string AccountType { get; set; }

        [Required]
        [MaxLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string AccountPricingPlan { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }

        [Required]
        public DateTime? UpdatedDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public string DetailedMessage { get; set; }
    }
}
