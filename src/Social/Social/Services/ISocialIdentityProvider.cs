using Social.ViewModels;

namespace Social.Services
{
    public interface ISocialIdentityProvider
    {
        SocialIdentityViewModel[] GetSupportedIdentities();
    }
}
