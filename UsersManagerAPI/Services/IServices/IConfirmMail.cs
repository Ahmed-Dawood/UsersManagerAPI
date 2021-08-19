using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.Services.IServices
{
    public interface IConfirmMail
    {
        Task<IUserInfo> SendConfirmEmailAsync(IUserInfo userInfo);

        Task<IUserInfo> UpdateConfirmMailAsync(IUserInfo userInfo);
    }
}
