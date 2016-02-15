using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

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

        private static string UserSettingsFile
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

        private static UserSettings LoadUserSettingsFile()
        {
            //return XDocument.Load(UserSettingsFile);

            XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));

            // Open the file and deserialize the contents
            using (FileStream fs = new FileStream(UserSettingsFile, FileMode.Open))
            {
                return (UserSettings)serializer.Deserialize(fs);
            }
        }

        private static void SaveUserSettingsFile(UserSettings settings)
        {
            Utility.SerializeObject<UserSettings>(settings, UserSettingsFile);
        }

        /// <summary>
        /// Creates empty user settings file
        /// </summary>
        public static void CreateEmptyUserSettingsFile()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement element1 = doc.CreateElement("", XMLCONFIG_ELEMENT_ROOT, "");
            doc.AppendChild(element1);

            XmlElement element2 = doc.CreateElement("", XMLCONFIG_ELEMENT_FWFILE, "");
            element1.AppendChild(element2);

            XmlElement element3 = doc.CreateElement("", XMLCONFIG_ELEMENT_SELGRPS, "");
            element1.AppendChild(element3);

            doc.Save(UserSettingsFile);
        }

        public static void SaveFirewallsFile(string firewallsfile, UserSettings settings)
        {
            XDocument xdoc = XDocument.Load(UserSettingsFile);

            if (xdoc != null)
            {
                var element = xdoc.Descendants(XMLCONFIG_ELEMENT_FWFILE).Single();
                if (element != null)
                {
                    element.Value = firewallsfile;
                    xdoc.Save(UserSettingsFile);
                }
            }
        }

        public static String GetFirewallsFile()
        {
            if (!File.Exists(UserSettingsFile))
            {
                CreateEmptyUserSettingsFile();
            }

            UserSettings settings = LoadUserSettingsFile();
            return settings.FirewallFilename;
        }

        public static List<String> GetSavedGroups()
        {
            if (!File.Exists(UserSettingsFile))
            {
                CreateEmptyUserSettingsFile();
            }

            UserSettings settings = LoadUserSettingsFile();

            if (!string.IsNullOrWhiteSpace(settings.SelectedGroups))
            {
                return settings.SelectedGroups.Split(',').ToList();
            }

            return null;
        }

        public static void SaveSelectedGroups(List<String> selectedgroups)
        {
            if (!File.Exists(UserSettingsFile))
            {
                CreateEmptyUserSettingsFile();
            }

            UserSettings settings = LoadUserSettingsFile();
            settings.SelectedGroups = string.Join(",", selectedgroups);

            SaveUserSettingsFile(settings);
        }
    }
}
