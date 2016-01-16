using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FirewallAuthenticator
{
    /// <summary>
    /// Business object class for managing Firewall Groups
    /// </summary>
    public class FirewallGroupManager
    {
        public static Dictionary<String, List<String>> GetFirewallGroups(string xmlfile)
        {
            XDocument xdoc = XDocument.Load(xmlfile);
            Dictionary<String, List<String>> fwgrps = null;

            if (xdoc != null)
            {
                fwgrps = new Dictionary<String, List<String>>();

                var fwgrpnames = xdoc.Descendants("firewalls").Elements("firewall").Attributes().Select(x => x.Value).ToList();

                if (fwgrpnames != null && fwgrpnames.Count() > 0)
                {
                    foreach (var fwgrpname in fwgrpnames)
                    {
                        if (fwgrps.ContainsKey(fwgrpname))
                        {
                            throw new ApplicationException(String.Format("The source xmlfile contains more than one group with the name '{0}'.\n Please remove or rename this group.", fwgrpname));
                        }
                        else
                        {
                            IEnumerable<XElement> urls = xdoc.Descendants("firewall").Where(x => (string)x.Attribute("groupname") == fwgrpname).Elements();

                            if (urls != null && urls.Count() > 0)
                            {
                                fwgrps.Add(fwgrpname.ToString(), urls.Select(el => el.Value).ToList());
                            }
                            else
                            {
                                throw new ApplicationException(String.Format("The firewall group '{0}' does not appear to have any URLs.\n Please add URLs to this group or remove it.", fwgrpname));
                            }
                        }
                    }
                }
            }

            return fwgrps;
        }

        public static List<FirewallGroup> GetSelectedFirewallGroups(Dictionary<String, List<String>> FirewallGroups, IList SelectedFirewallGroups)
        {
            List<FirewallGroup> fwgrps = null;

            if (FirewallGroups != null)
            {
                fwgrps = new List<FirewallGroup>();
                FirewallGroup fwgrp = null;

                foreach (string grp in SelectedFirewallGroups)
                {
                    fwgrp = new FirewallGroup();
                    fwgrp.GroupName = grp.ToString();
                    fwgrp.URLs = new List<string>();

                    var selectedgroups = FirewallGroups.Where(selectedgrp => selectedgrp.Key == grp);

                    if (selectedgroups != null)
                    {
                        foreach (var selectedgroup in selectedgroups)
                        {
                            fwgrp.URLs.AddRange(selectedgroup.Value);
                            fwgrps.Add(fwgrp);
                        }
                    }
                }

            }

            return fwgrps;
        }
    }
}
