using SimpleAuthentication.Core;
using Social.ViewModels;

namespace Social.Services
{
    public interface ILoginService
    {
        LoginResult LoginUser(UserInformation userInformation);
        LogoutResult LogoutUser();
    }
}
