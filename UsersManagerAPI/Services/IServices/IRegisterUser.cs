using System.Threading.Tasks;
using UsersManagerAPI.DomainClasses.Models.IModels;

namespace UsersManagerAPI.IServices
{
    public interface IRegisterUser
    {
        Task<IUserInfo> SignUp(IUserInfo RegisterInfo);
    }
}
