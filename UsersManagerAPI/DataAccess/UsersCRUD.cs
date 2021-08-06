using System;
using System.Collections.Generic;
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

        public UserInfo GetUserCredentials(string userEmail)
        {
            UserInfo userinfo = new UserInfo();
            try
            {
                userinfo = UsersBD.Users
                    .Where(u => u.Email == userEmail)
                    .Select(x => new UserInfo { 
                        SaltKey = x.SaltKey,
                        HashPassword = x.HashPassword })
                    .First();
                userinfo.Email = userEmail;
                userinfo.Message = Message.Success;
            }
            catch (Exception ex)
            {
                userinfo.Message = Message.ErrorFound;
                userinfo.DetailedMessage = ex.InnerException.Message;
            }
            return userinfo;
        }

        async public Task<UserInfo> AddUserAsync(UserInfo userInfo)
        {
            try
            {
                try
                {
                    var userinfo = UsersBD.Users.Where(u => u.Email == userInfo.Email).First();
                    userInfo.Message = Message.UserAlreadyExist;
                }
                catch (Exception)
                {
                    await UsersBD.Users.AddAsync(userInfo);
                    await UsersBD.SaveChangesAsync();
                    userInfo.Message = Message.Success;
                }                
            }
            catch (Exception ex)
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = ex.InnerException.Message;
            }
            return userInfo;
        }

        async public Task<UserInfo> DeleteUser(string userEmail)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                var userinfo = UsersBD.Users.Where(u => u.Email == userEmail).FirstOrDefault();
                userinfo.IsDeleted = true;
                await UsersBD.SaveChangesAsync();
                userinfo.Message = Message.UserRemoved;
            }
            catch (Exception ex)
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = ex.InnerException.Message;
            }
            return userInfo;
        }

        async public Task<UserInfo> UpdateUser(UserInfo userInfo, IEnumerable<UpdatableInfoEnum> UpdatedItems)
        {
            try
            {
                var userinfo = UsersBD.Users.Where(u => u.Email == userInfo.Email).FirstOrDefault();
                foreach (var item in UpdatedItems)
                {
                    switch (item)
                    {
                        case UpdatableInfoEnum.FirstName:
                            userinfo.FirstName = userInfo.FirstName;
                            break;
                        case UpdatableInfoEnum.LastName:
                            userinfo.LastName = userInfo.LastName;    
                            break;
                        case UpdatableInfoEnum.Email:
                            userinfo.Email = userInfo.Email;
                            break;
                        case UpdatableInfoEnum.Password:
                            userinfo.Password = userInfo.Password;
                            break;
                        case UpdatableInfoEnum.IsMailConfirmed:
                            userinfo.IsMailConfirmed = userInfo.IsMailConfirmed;
                            break;
                        case UpdatableInfoEnum.Role:
                            userinfo.Role = userInfo.Role;
                            break;
                        case UpdatableInfoEnum.AccountType:
                            userinfo.AccountType = userInfo.AccountType;
                            break;
                        case UpdatableInfoEnum.AccountPricingPlan:
                            userinfo.AccountPricingPlan = userInfo.AccountPricingPlan;
                            break;
                        case UpdatableInfoEnum.UpdatedDate:
                            userinfo.UpdatedDate = userInfo.UpdatedDate;
                            break;
                    }
                }
                await UsersBD.SaveChangesAsync();
                userinfo.Message = Message.Success;
            }
            catch (Exception ex)
            {
                userInfo.Message = Message.ErrorFound;
                userInfo.DetailedMessage = ex.InnerException.Message;
            }
            return userInfo;
        }
    }
}
