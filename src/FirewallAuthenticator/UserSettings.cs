using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FirewallAuthenticator
{
    [Serializable]
    [XmlRoot(ElementName = "fw_auth_cfg")]
    public class UserSettings
    {
        [XmlElement(ElementName = "firewall_file")]
        public string FirewallFilename { get; set; }
        [XmlElement(ElementName = "selected_groups")]
        public string SelectedGroups { get; set; }
    }
}
