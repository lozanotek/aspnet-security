using IdentityModel.Client;

var url = "https://mua.kapu.io";

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync(url);
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(
    new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "demo-client",
    ClientSecret = "secret",
    Scope = "demo-api"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine("-- Access Token --");
Console.WriteLine(tokenResponse.AccessToken);

Console.Write("Press any key to continue...");
Console.WriteLine();
Console.ReadKey(true);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:7160/identity");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine("-- API Error --");
    Console.WriteLine(response.StatusCode);
}
else
{
    Console.WriteLine("-- API Content --");
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}

Console.Write("Press any key to exit...");
Console.ReadKey(true);
