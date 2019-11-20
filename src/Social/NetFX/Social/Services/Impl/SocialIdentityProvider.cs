using System.Linq;
using SimpleAuthentication.Core.Config;
using Social.ViewModels;

namespace Social.Services
{
    public class SocialIdentityProvider : ISocialIdentityProvider
    {
        public SocialIdentityViewModel[] GetSupportedIdentities()
        {
            var config = ProviderConfigHelper.UseConfig();
            if (config == null)
            {
                return new SocialIdentityViewModel[0];
            }

            var providers = config.Providers;

            if (providers == null || providers.Count == 0)
            {
                return new SocialIdentityViewModel[0];
            }

            var providerNames = providers.Select(p => p.Name).ToArray();

            var socialList = providerNames.Select(providerName =>
                new SocialIdentityViewModel
                {
                    Id = providerName.ToLower(),
                    CssName = providerName.ToLower(),
                    Name = providerName
                }).ToArray();

            return socialList;
        }
    }
}
