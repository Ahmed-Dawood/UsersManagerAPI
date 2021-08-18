using System;
using System.Linq;
using System.Threading.Tasks;
using UsersManagerAPI.DataAccess.IDataAccess;
using UsersManagerAPI.DomainClasses.Common;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.DataAccess
{
    public class UsersCRUD : IUsersCRUD
    {
        private UsersBDContext UsersBD { get; }

        public UsersCRUD(UsersBDContext usersBD)
        {
            UsersBD = usersBD;
        }        

        public UserInfo GetUser(string userName)
        {
            UserInfo userinfo = new UserInfo();
            try
            {
                userinfo = UsersBD.Users
                    .Where(u => u.UserName == userName)                  
                    .First();
                userinfo.Message = Message.Success;
            }
            catch
            {
                userinfo.Message = Message.ErrorFound;
                userinfo.DetailedMessage = "Error in GetUser method in UsersCRUD class";
            }
            return userinfo;
        }

        async public Task<UserInfo> AddUserAsync(UserInfo userInfo)
        {
            try
            {
                try
                {
                    var userinfo = UsersBD.Users.Where(u => u.UserName == userInfo.UserName).First();
                    userInfo.Message = Message.UserAlreadyExist;
                }
                catch (Exception)
                {
                    await UsersBD.Users.AddAsync(userInfo);
                    await UsersBD.SaveChangesAsync();
                    userInfo.Message = Message.Success;
                }                
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = "Error in AddUserAsync method in UsersCRUD class";
            }
            return userInfo;
        }

        async public Task<UserInfo> DeleteUser(string userName)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                var userinfo = UsersBD.Users.Where(u => u.UserName == userName).FirstOrDefault();
                userinfo.IsDeleted = true;
                await UsersBD.SaveChangesAsync();
                userinfo.Message = Message.UserRemoved;
            }
            catch (Exception ex)
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = "Error in DeleteUser method in UsersCRUD class";
            }
            return userInfo;
        }

        async public Task<UserInfo> UpdateUser(UserInfo userInfo)
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
            catch (Exception ex)
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = "Error in UpdateUser method in UsersCRUD class";
            }
            return userInfo;
        }
    }
}
