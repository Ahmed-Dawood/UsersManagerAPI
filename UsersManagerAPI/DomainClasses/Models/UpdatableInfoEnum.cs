using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersManagerAPI.DomainClasses.Models
{
    public enum UpdatableInfoEnum
    {
        FirstName,
        LastName,
        UserName,
        Email,
        Password,
        IsMailConfirmed,
        Role,
        AccountType,
        AccountPricingPlan,
        UpdatedDate
    }
}
