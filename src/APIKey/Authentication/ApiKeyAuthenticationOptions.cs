using Microsoft.AspNetCore.Authentication;

namespace APIKey.Authentication;

public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = ApiKeyDefaults.AuthenticationScheme;

	// Default header prefix
	public string HeaderPrefix { get; set; } = "API-KEY ";

	// Default header name
	public string HeaderName { get; set; } = "Authorization";

	public string? DefaultApiKey { get; set; } = null;
}
