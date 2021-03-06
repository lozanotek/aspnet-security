using System.Security.Principal;

namespace ApiAuth.Security
{
    public class CallerIdentity : IIdentity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthenticationType => "Custom";
        public bool IsAuthenticated => true;
    }
}
