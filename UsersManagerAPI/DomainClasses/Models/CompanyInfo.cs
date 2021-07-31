using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagerAPI.DomainClasses.Models
{
    public class CompanyInfo
    {
        [Key]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        [ForeignKey("UserId")]
        public UserInfo AdminUser { get; set; }

        public string CompanyPricingPlan { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<UserInfo> CompanyEmployees { get; set; }

        [NotMapped]
        public string Messege { get; set; }
    }
}
