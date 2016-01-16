using System;
using System.Collections.Generic;

namespace FirewallAuthenticator
{
    public class FirewallGroup
    {
        public String GroupName { get; set; }
        public List<String> URLs { get; set; }
    }
}
