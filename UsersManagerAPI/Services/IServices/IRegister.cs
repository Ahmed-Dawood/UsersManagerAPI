using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.IServices
{
    public interface IRegister
    {
        Task<UserInfo> SignUp(RegisterInfo RegisterInfo);
        Task<string> ConfirmMail(string UserName);
    }
}
