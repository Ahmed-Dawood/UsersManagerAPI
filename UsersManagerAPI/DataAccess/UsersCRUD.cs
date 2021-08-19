using Microsoft.EntityFrameworkCore;
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

        async public Task<IUserInfo> GetUserAsync(IUserInfo userinfo)
        {
            userinfo = await UsersBD.Users
                .FirstOrDefaultAsync(u => u.UserName == userinfo.UserName);
            if (userinfo != null)
            {
                userinfo.Message = Message.Success;
            }
            else
            {
                userinfo.Message = Message.ErrorFound;
                userinfo.DetailedMessage = userinfo.DetailedMessage + " - Error in GetUser method in UsersCRUD class";
            }
            return userinfo;
        }

        async public Task<IUserInfo> AddUserAsync(IUserInfo userInfo)
        {
            try
            {
                var userinfo = await UsersBD.Users.FirstOrDefaultAsync(u => u.UserName == userInfo.UserName);
                if (userinfo == null)
                {
                    userinfo = await UsersBD.Users.FirstOrDefaultAsync(u => u.Email == userInfo.Email);
                    if (userinfo == null)
                    {
                        await UsersBD.Users.AddAsync((UserInfo)userInfo);
                        await UsersBD.SaveChangesAsync();
                        userInfo.Message = Message.Success;
                    }
                    else
                    {
                        userInfo.Message = Message.DuplicateEmail;
                    }
                }
                else if (userInfo.IsDeleted == true)
                {
                    await UpdateUserAsync(userInfo);
                }
                else
                {
                    userInfo.Message = Message.UserAlreadyExist;
                }                
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = userInfo.DetailedMessage + " - Error in AddUserAsync method in UsersCRUD class";
            }
            return userInfo;
        }

        async public Task<IUserInfo> DeleteUserAsync(IUserInfo userInfo)
        {
            try
            {
                var userinfo = await UsersBD.Users.FirstOrDefaultAsync(u => u.UserName == userInfo.UserName);
                if (userinfo == null)
                {
                    userInfo.Message = Message.InvalidUser;
                }
                else
                {
                    userinfo.IsDeleted = true;
                    userinfo.UpdatedDate = DateTime.Now;
                    await UsersBD.SaveChangesAsync();
                    userInfo.Message = Message.Success;
                }
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = userInfo.DetailedMessage + " - Error in DeleteUser method in UsersCRUD class";
            }
            return userInfo;
        }

        async public Task<IUserInfo> UpdateUserAsync(IUserInfo userInfo)    
        {
            try
            {
                var userinfo = await UsersBD.Users.FirstOrDefaultAsync(u => u.UserName == userInfo.UserName);
                if (userinfo == null)
                {
                    userInfo.Message = Message.InvalidUser;
                }
                else
                {
                    userinfo.FirstName = userInfo.FirstName;
                    userinfo.LastName = userInfo.LastName;
                    userinfo.Email = userInfo.Email;
                    userinfo.HashPassword = userInfo.HashPassword;
                    userinfo.SaltKey = userInfo.SaltKey;
                    userinfo.IsMailConfirmed = userInfo.IsMailConfirmed;
                    userinfo.Role = userInfo.Role;
                    userinfo.AccountType = userInfo.AccountType;
                    userinfo.AccountPricingPlan = userInfo.AccountPricingPlan;
                    userinfo.UpdatedDate = DateTime.Now;
                    await UsersBD.SaveChangesAsync();
                    userInfo.Message = Message.Success;
                }
            }
            catch
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = userInfo.DetailedMessage + " - Error in UpdateUser method in UsersCRUD class";
            }
            return userInfo;
        }
    }
}
