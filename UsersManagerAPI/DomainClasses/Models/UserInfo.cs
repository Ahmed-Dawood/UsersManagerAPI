using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class UserInfo : RegisterInfo
    {
        //WIP
        [Key]
        public int UserId { get; set; }

        [Required]
        public string HashPassword { get; set; }

        [Required]
        public string SaltKey { get; set; }

        [Required]
        public bool IsMailConfirmed { get; set; }

        public string Role { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
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
