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
        public UsersBDContext UsersBD { get; }

        public UsersCRUD(UsersBDContext usersBD)
        {
            UsersBD = usersBD;
        }        

        public UserInfo GetUser(string userEmail)
        {
            UserInfo userinfo = null;
            try
            {
                userinfo  = UsersBD.Users.Where(u => u.Email == userEmail).First();
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

        async public Task<UserInfo> UpdateUser(UserInfo userInfo)
        {
            try
            {
                var userinfo = UsersBD.Users.Where(u => u.Email == userInfo.Email).FirstOrDefault();
                userinfo.FirstName = userInfo.FirstName;
                userinfo.LastName = userInfo.LastName;
                userinfo.Password = userInfo.Password;
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
    }
}
