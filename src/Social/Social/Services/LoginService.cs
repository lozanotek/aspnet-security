using System.Web.Security;
using SimpleAuthentication.Core;
using Social.ViewModels;

namespace Social.Services
{
    public class LoginService : ILoginService
    {
        public LoginResult LoginUser(UserInformation userInformation)
        {
            FormsAuthentication.SetAuthCookie(userInformation.UserName, false);
            return new LoginResult { RedirectUrl = FormsAuthentication.DefaultUrl };
        }

        public LogoutResult LogoutUser()
        {
            FormsAuthentication.SignOut();
            return new LogoutResult { RedirectUrl = FormsAuthentication.DefaultUrl };
        }
    }
}
