namespace APIKey.Authentication;
public class AppSettingsApiKeyValidator : IApiKeyValidator
{
    private readonly IConfiguration configuration;

    public AppSettingsApiKeyValidator(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Task<ValidationResult> Validate(string apiKey)
    {
        try
        {
            var storedApKey = configuration.GetValue<string>("Authentication:ApiKey");
            var result = new ValidationResult
            {
                IsValid = string.Equals(apiKey, storedApKey, StringComparison.CurrentCulture)
            };

            return Task.FromResult(result);
        }
        catch
        {
            return Task.FromResult(new ValidationResult { IsValid = false});
        }
    }
}
