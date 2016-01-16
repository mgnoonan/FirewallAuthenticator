using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace FirewallAuthenticator
{
    public class XMLFileManager
    {

        private const String XMLCONFIG_ELEMENT_ROOT = "fw_auth_cfg";
        private const String XMLCONFIG_ELEMENT_FWFILE = "firewall_file";
        private const String XMLCONFIG_ELEMENT_SELGRPS = "selected_groups";

        private const String XMLFWFILE_ELEMENT_ROOT = "firewalls";
        private const String XMLFWFILE_ELEMENT_FW = "firewall";
        private const String XMLFWFILE_ELEMENT_FW_ATTRIB_GRPNAME = "groupname";
        private const String XMLFWFILE_ELEMENT_FWURL = "URL";

        //private static String FirewallsFile = String.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, "firewallfile.xml");

        private static string ConfigFile
        {
            get
            {
                string filename = "fwa_cfg.xml";

                // use user's temporary env dir if set
                if (!String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User)))
                    return Path.Combine(Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.User), filename);

                if (!String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User)))
                    return Path.Combine(Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User), filename);

                // otherwise hardcode
                return String.Format(@"C:\temp\{0}", filename);

            }

        }

        public static String CreateXMLFirewallFile()
        {

            string firewallfile = "myfirewalls.xml";

            // use user's temporary env dir if set
            if (!String.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("USERPROFILE")))
                firewallfile = Path.Combine(System.Environment.GetEnvironmentVariable("USERPROFILE"), firewallfile);
            else
                // otherwise hardcode
                firewallfile = String.Format(@"C:\{0}", firewallfile);

            XmlDocument doc = new XmlDocument();

            XmlElement element1 = doc.CreateElement("", XMLFWFILE_ELEMENT_ROOT, "");
            doc.AppendChild(element1);

            XmlElement element2 = doc.CreateElement("", XMLFWFILE_ELEMENT_FW, "");
            element2.SetAttribute(XMLFWFILE_ELEMENT_FW_ATTRIB_GRPNAME, String.Format(@"put group name here - add as many {0} elements with a {1} attribute as you need.", XMLFWFILE_ELEMENT_FW, XMLFWFILE_ELEMENT_FW_ATTRIB_GRPNAME));
            element1.AppendChild(element2);

            XmlElement element3 = doc.CreateElement("", XMLFWFILE_ELEMENT_FWURL, "");
            XmlText text1 = doc.CreateTextNode(String.Format(@"put firewall url here - add as many {0} elements for this firewall group as you need.", XMLFWFILE_ELEMENT_FWURL));
            element3.AppendChild(text1);
            element2.AppendChild(element3);

            doc.Save(firewallfile);

            return firewallfile;
        }

        /// <summary>
        /// creates config file to store common info for the app
        /// </summary>
        public static void CreateConfigXMLFile()
        {
            if (!File.Exists(ConfigFile))
            {

                XmlDocument doc = new XmlDocument();

                XmlElement element1 = doc.CreateElement("", XMLCONFIG_ELEMENT_ROOT, "");
                doc.AppendChild(element1);

                XmlElement element2 = doc.CreateElement("", XMLCONFIG_ELEMENT_FWFILE, "");
                element1.AppendChild(element2);

                XmlElement element3 = doc.CreateElement("", XMLCONFIG_ELEMENT_SELGRPS, "");
                element1.AppendChild(element3);

                doc.Save(ConfigFile);
            }
        }

        public static void SaveFirewallsFile(string firewallsfile)
        {
            XDocument xdoc = XDocument.Load(ConfigFile);

            if (xdoc != null)
            {
                var element = xdoc.Descendants(XMLCONFIG_ELEMENT_FWFILE).Single();
                if (element != null)
                {
                    element.Value = firewallsfile;
                    xdoc.Save(ConfigFile);
                }
            }
        }

        public static String GetFirewallsFile()
        {
            CreateConfigXMLFile();

            XDocument xdoc = XDocument.Load(ConfigFile);

            if (xdoc != null)
            {
                var element = xdoc.Descendants(XMLCONFIG_ELEMENT_FWFILE).Single();

                if (element != null)
                {
                    return element.Value.ToString();
                }
            }

            return null;

        }

        public static List<String> GetSavedGroups()
        {
            CreateConfigXMLFile();

            XDocument xdoc = XDocument.Load(ConfigFile);

            if (xdoc != null)
            {
                var element = xdoc.Descendants(XMLCONFIG_ELEMENT_SELGRPS).Single();

                if (element != null)
                {
                    return element.Value.Split(',').ToList();
                }
            }

            return null;

        }

        public static void SaveSelectedGroups(List<String> selectedgroups)
        {
            XDocument xdoc = XDocument.Load(ConfigFile);

            if (xdoc != null)
            {
                var element = xdoc.Descendants(XMLCONFIG_ELEMENT_SELGRPS).Single();
                if (element != null)
                {
                    element.Value = String.Join(",", selectedgroups);
                    xdoc.Save(ConfigFile);
                }
            }

        }

    }
}
