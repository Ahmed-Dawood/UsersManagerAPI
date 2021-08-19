using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.Services.IServices
{
    public interface IMailService
    {
        Task<string> SendMailAsync(MailClass mailClass);
        string GetMailBody(IUserInfo userInfo);
    }
}
