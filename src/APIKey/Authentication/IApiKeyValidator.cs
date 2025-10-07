namespace APIKey.Authentication;

public interface IApiKeyValidator
{
    Task<ValidationResult> Validate(string apiKey);
}
