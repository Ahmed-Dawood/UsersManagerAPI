using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.IServices
{
    public interface IMailService
    {
        Task<string> SendMail(MailClass mailClass);
        string GetMailBody(string username);
    }
}
