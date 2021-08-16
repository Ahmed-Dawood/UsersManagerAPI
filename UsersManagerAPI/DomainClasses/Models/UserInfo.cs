using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersManagerAPI.DomainClasses.Models.IModels;

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
