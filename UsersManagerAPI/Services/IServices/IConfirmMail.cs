using System.Threading.Tasks;

namespace UsersManagerAPI.Services.IServices
{
    public interface IConfirmMail
    {
        Task<string> ConfirmEmail(string UserName);
    }
}
