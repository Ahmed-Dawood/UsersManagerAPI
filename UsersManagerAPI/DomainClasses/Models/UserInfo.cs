using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class UserInfo : RegisterInfo
    {
        [Key]
        public int UserId { get; set; }

        public bool IsMailConfirmed { get; set; }

        public string Role { get; set; }

        public string AccountType { get; set; }

        public string AccountPricingPlan { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        public string Messege { get; set; }
    }
}
