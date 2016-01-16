using System;

namespace FirewallAuthenticator
{
    public enum AuthResultType
    {
        PASS = 1,
        FAIL = 2
    }

    public class AuthResult
    {
        public String Message { get; set; }
        public AuthResultType Type { get; set; }
    }
}
