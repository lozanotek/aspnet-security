namespace IdentityClient
{
    public static class SecurityConstants
    {
        public const string HydridResponseType = "code id_token token";
        public const string ImplicitResponseType = "id_token token";
        public const string Scope = "openid profile email";
        public const string AuthenticationType = "Cookies";

        //public const string AuthorityUri = "http://localhost:1337/identity";
        public const string AuthorityUri = "http://identity.lozanotek.net/identity";
        public const string UserInfoUri = AuthorityUri + "/connect/userinfo";
    }
}
