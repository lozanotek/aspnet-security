using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;

using APIKey.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiKeyExtensions
{
    public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder, Action<ApiKeyAuthenticationOptions>? configureOptions = null)
	{
        var services = builder.Services;
        
        services.TryAddTransient<IApiKeyValidator, AppSettingsApiKeyValidator>();
        return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyDefaults.AuthenticationScheme, configureOptions);
	}
}
