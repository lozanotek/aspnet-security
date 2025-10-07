using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace APIKey.Authentication;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
	private readonly IApiKeyValidator keyValidator;

	public ApiKeyAuthenticationHandler(
		IOptionsMonitor<ApiKeyAuthenticationOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
		IApiKeyValidator keyValidator)
		: base(options, logger, encoder)
	{
		this.keyValidator = keyValidator;
	}

	protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		// If the endpoint allows anonymous access, skip auth
		var endpoint = Context.GetEndpoint();
		if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
		{
			return AuthenticateResult.NoResult();
		}

		// Get API key from request based on options
		var apiKey = GetApiKeyFromRequestUsingOptions(Request);

		// If no API key found, no further work possible
		if (string.IsNullOrEmpty(apiKey))
		{
			return AuthenticateResult.NoResult();
		}

		var defaultKey = Options.DefaultApiKey;
		if (!string.IsNullOrWhiteSpace(defaultKey))
		{
			if (!string.Equals(apiKey, defaultKey, StringComparison.CurrentCulture))
			{
				return AuthenticateResult.Fail("Invalid API Key.");
			}
		}
		else
		{
			// Validate
			var result = await keyValidator.Validate(apiKey);
			if (result == null)
			{
				return AuthenticateResult.Fail("Could not validate API Key.");
			}

			if (!result.IsValid)
			{
				return AuthenticateResult.Fail("Invalid API Key.");
			}
		}

		var principal = CreatePrincipal(apiKey);
		var ticket = new AuthenticationTicket(principal, Scheme.Name);

		return AuthenticateResult.Success(ticket);
	}

	protected virtual ClaimsPrincipal CreatePrincipal(string apiKey)
	{
		var clientClaims = new[] { new Claim("apiKey", apiKey) };
		var identity = new ClaimsIdentity(clientClaims, Scheme.Name);
		var principal = new ClaimsPrincipal(identity);

		return principal;
	}

	protected virtual string? GetApiKeyFromRequestUsingOptions(HttpRequest request)
	{
		string? requestApiKey = null;
		var headerName = Options.HeaderName;

		// If using custom header, get the API key from there
		if (!string.Equals(HeaderNames.Authorization, headerName, StringComparison.OrdinalIgnoreCase))
		{
			requestApiKey = Request.Headers[Options.HeaderName].FirstOrDefault();
		}
		else
		{
			// Get API key from Authorization header
			var authHeader = Request.Headers[HeaderNames.Authorization].FirstOrDefault();

			// If no authorization header found, nothing to process further
			if (string.IsNullOrEmpty(authHeader))
			{
				return null;
			}

			if (authHeader.StartsWith(Options.HeaderPrefix, StringComparison.OrdinalIgnoreCase))
			{
				int spaceIndex = !Options.HeaderPrefix.EndsWith(" ", StringComparison.Ordinal) ? 1 : 0;
				int keyStartIndex = Options.HeaderPrefix.Length + spaceIndex;

				requestApiKey = authHeader.Substring(keyStartIndex).Trim();
			}
		}

		return requestApiKey;
	}
}
