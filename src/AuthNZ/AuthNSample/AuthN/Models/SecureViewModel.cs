using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AuthN.Models;

public class SecureViewModel
{
    public SecureViewModel(AuthenticateResult result)
    {
        AuthenticateResult = result;
    }

     public string? AuthScheme
     {
         get
         {
             var properties = AuthenticateResult.Properties;
             var items = properties?.Items;

             return items != null && items.ContainsKey(".AuthScheme") ? items[".AuthScheme"] : string.Empty;
         }
     }

    public AuthenticateResult AuthenticateResult { get; }
    public ClaimsPrincipal? Principal => AuthenticateResult.Principal;
}