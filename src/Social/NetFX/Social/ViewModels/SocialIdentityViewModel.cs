using System;
using SimpleAuthentication.Core;

namespace Social.ViewModels
{
    public class SocialIdentityViewModel
    {
        public string Name { get; set; }
        public string CssName { get; set; }
        public string Id { get; set; }
    }

    public class SocialIdentityInputModel
    {
        public string Id { get; set; }
    }

    public class LoginResult
    {
        public string RedirectUrl { get; set; }
    }

    public class LogoutResult
    {
        public string RedirectUrl { get; set; }
    }

    public class CallbackViewModel
    {
        public IAuthenticatedClient AuthenticatedClient { get; set; }
        public Exception Exception { get; set; }
        public string ReturnUrl { get; set; }
    }
}
