using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.IServices
{
    public interface IRegisterUser
    {
        Task<UserInfo> SignUp(UserInfo RegisterInfo);
    }
}
