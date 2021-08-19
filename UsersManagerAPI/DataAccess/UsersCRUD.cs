using System;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.DataAccess
{
    public class UsersCRUD : IUsersCRUD
    {
        private UsersBDContext UsersBD { get; }

        public UsersCRUD(UsersBDContext usersBD)
        {
            UsersBD = usersBD;
        }        

        public IUserInfo GetUser(IUserInfo userinfo)
        {
            try
            {
                userinfo = UsersBD.Users
                    .Where(u => u.UserName == userinfo.UserName)                  
                    .First();
                userinfo.Message = Message.Success;
            }
            catch
            {
                userinfo.Message = Message.ErrorFound;
                userinfo.DetailedMessage = userinfo.DetailedMessage + "\nError in GetUser method in UsersCRUD class";
            }
            return userinfo;
        }

        async public Task<IUserInfo> AddUserAsync(IUserInfo userInfo)
        {
            try
            {
                try
                {
                    var userinfo = UsersBD.Users.Where(u => u.UserName == userInfo.UserName).First();
                    userInfo.Message = Message.UserAlreadyExist;
                }
                catch
                {
                    await UsersBD.Users.AddAsync((UserInfo)userInfo);
                    await UsersBD.SaveChangesAsync();
                    userInfo.Message = Message.Success;
                }                
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = userInfo.DetailedMessage + "\nError in AddUserAsync method in UsersCRUD class";
            }
            return userInfo;
        }

        async public Task<IUserInfo> DeleteUser(IUserInfo userInfo)
        {
            try
            {
                var userinfo = UsersBD.Users.Where(u => u.UserName == userInfo.UserName).FirstOrDefault();
                userinfo.IsDeleted = true;
                await UsersBD.SaveChangesAsync();
                userinfo.Message = Message.UserRemoved;
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = userInfo.DetailedMessage + "\nError in DeleteUser method in UsersCRUD class";
            }
            return userInfo;
        }

        async public Task<IUserInfo> UpdateUser(IUserInfo userInfo)
        {
            try
            {
                var userinfo = UsersBD.Users.Where(u => u.UserName == userInfo.UserName).FirstOrDefault();
                userinfo.FirstName = userInfo.FirstName;
                userinfo.LastName = userInfo.LastName;    
                userinfo.Email = userInfo.Email;
                userinfo.HashPassword = userInfo.HashPassword;
                userinfo.SaltKey = userInfo.SaltKey;
                userinfo.IsMailConfirmed = userInfo.IsMailConfirmed;
                userinfo.Role = userInfo.Role;
                userinfo.AccountType = userInfo.AccountType;
                userinfo.AccountPricingPlan = userInfo.AccountPricingPlan;
                userinfo.UpdatedDate = userInfo.UpdatedDate;
                await UsersBD.SaveChangesAsync();
                userinfo.Message = Message.Success;
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = userInfo.DetailedMessage + "\nError in UpdateUser method in UsersCRUD class";
            }
            return userInfo;
        }
    }
}
