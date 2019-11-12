using System.Security.Principal;

namespace ApiAuth.Security
{
    public class SecurityPrincipal : IPrincipal
    {
        public SecurityPrincipal(CallerIdentity identity)
        {
            Caller = identity;
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public CallerIdentity Caller { get; private set; }
        public IIdentity Identity { get { return Caller;} }
    }
}