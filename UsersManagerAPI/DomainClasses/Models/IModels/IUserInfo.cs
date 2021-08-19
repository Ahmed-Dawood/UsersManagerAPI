using System;

namespace UsersManagerAPI.DomainClasses.Models.IModels
{
    public interface IUserInfo
    {
        string AccountPricingPlan { get; set; }
        string AccountType { get; set; }
        string ConfirmPassword { get; set; }
        DateTime? CreatedDate { get; set; }
        string DetailedMessage { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string HashPassword { get; set; }
        bool IsDeleted { get; set; }
        bool IsMailConfirmed { get; set; }
        string LastName { get; set; }
        string Message { get; set; }
        string Password { get; set; }
        string Role { get; set; }
        string SaltKey { get; set; }
        DateTime? UpdatedDate { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
    }
}