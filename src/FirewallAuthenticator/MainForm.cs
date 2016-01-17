using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WebBrowserControlDialogs;

namespace FirewallAuthenticator
{
    public partial class frmMain : Form
    {
        [DllImport("Shell32.dll")]
        public extern static int ExtractIconEx(
            string libName,
            int iconIndex,
            IntPtr[] largeIcon,
            IntPtr[] smallIcon,
            uint nIcons
        );

        [DllImport("Shell32.dll")]
        public extern static IntPtr ExtractIcon(
            IntPtr appInstance,
            string libName,
            int iconIndex
        );
        // tooltip index
        int _ttIndex = -1;
        // global firewall groups
        Dictionary<String, List<String>> _fwgrps = null;

        Icon _iconLargeApplication = null;
        Icon _iconSmallApplication = null;
        Icon _iconSysTrayGood = null;
        Icon _iconSysTrayBad = null;
        Icon _iconSysTrayNotify = null;

        DateTime _lastAuth;
        string _passwordString = string.Empty;
        bool _ignoreSslErrors = true;

        public frmMain()
        {
            InitializeComponent();

            // Subscribe to Event(s) with the WindowsInterop Class
            WindowsInterop.SecurityAlertDialogWillBeShown +=
                new GenericDelegate<Boolean, Boolean>(this.WindowsInterop_SecurityAlertDialogWillBeShown);

            try
            {
                InitIcons();
                InitConfigFile();
                InitControls();
                InitUsername();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("The application encountered a problem.\r\nMessage: {0}", ex.Message));
            }
        }

        private bool WindowsInterop_SecurityAlertDialogWillBeShown(bool param)
        {
            // Return true to ignore and not show the 
            // "Security Alert" dialog to the user
            return _ignoreSslErrors;
        }

        private void InitIcons()
        {
            // Icons viewed from http://diymediahome.org/windows-icons-reference-list-with-details-locations-images/
            // Using the shield icons to show good/bad/notify states
            // %windir%\system32\imageres.dll

            // Dummy large icon
            Icon dummyLarge;
            // Padlock icons
            GetIconsFromDLL(@"%windir%\system32\imageres.dll", 54, out _iconLargeApplication, out _iconSmallApplication);
            // Green sheild
            GetIconsFromDLL(@"%windir%\system32\imageres.dll", 101, out dummyLarge, out _iconSysTrayGood);
            // Red sheild
            GetIconsFromDLL(@"%windir%\system32\imageres.dll", 100, out dummyLarge, out _iconSysTrayBad);
            // Yellow sheild
            GetIconsFromDLL(@"%windir%\system32\imageres.dll", 102, out dummyLarge, out _iconSysTrayNotify);

            if (_iconLargeApplication != null)
            {
                this.Icon = _iconLargeApplication;
            }
            if (_iconSysTrayNotify != null)
            {
                notifyIcon1.Icon = _iconSysTrayNotify;
            }
        }

        private void GetIconsFromDLL(string dllName, int iconIndex, out Icon largeIcon, out Icon smallIcon)
        {
            largeIcon = null;
            smallIcon = null;
            IntPtr[] largeIcons = new IntPtr[13];
            IntPtr[] smallIcons = new IntPtr[13];

            if (ExtractIconEx(dllName, iconIndex, largeIcons, smallIcons, 1) > 0)
            {
                if (largeIcons[0] != IntPtr.Zero)
                {
                    largeIcon = Icon.FromHandle(largeIcons[0]);
                }
                if (smallIcons[0] != IntPtr.Zero)
                {
                    smallIcon = Icon.FromHandle(smallIcons[0]);
                }
            }

            IntPtr icons = ExtractIcon(this.Handle, dllName, iconIndex);
            if (icons != IntPtr.Zero)
            {
                largeIcon = Icon.FromHandle(icons);
            }
        }

        private void browser_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;

            if (br.Document != null)
            {
                if (br.Url == e.Url)
                {
                    if (br.Document.GetElementById("STATE") != null)
                    {

                        var step = br.Document.GetElementById("STATE").GetAttribute("value");

                        // username page
                        if (step == "1")
                        {
                            // fill in username and click submit button
                            br.Document.GetElementById("DATA").SetAttribute("value", txtUsername.Text);
                            br.Document.GetElementsByTagName("INPUT")[3].InvokeMember("click");
                        }
                        // password page
                        else if (step == "2")
                        {
                            // fill in password and click submit button
                            br.Document.GetElementById("DATA").OuterHtml = String.Format("<INPUT value=\"{0}\" name=DATA>", _passwordString);
                            br.Document.GetElementsByTagName("INPUT")[3].InvokeMember("click");
                        }
                        // method page
                        else if (step == "3")
                        {
                            // click submit
                            br.Document.GetElementsByTagName("INPUT")[5].InvokeMember("click");
                        }
                    }
                    else
                    {
                        // no STATE element, so determine if valid login or invalid and requires relogin
                        AuthResult r = new AuthResult();

                        if (br.Document.Title.Contains("Authentication"))
                        {
                            // if there's an input submit button then login failed
                            var reloginButton = br.Document.GetElementsByTagName("INPUT");
                            if (reloginButton.Count > 0 && reloginButton[0].GetAttribute("value") == "Relogin")
                            {
                                // Relogin button is present
                                r.Message = String.Format("FAILED: {0} - Perhaps a bad username or password.", br.Url.ToString());
                                r.Type = AuthResultType.FAIL;
                            }
                            else
                            {
                                String numberOfRules = "0 rules";
                                if (br.DocumentText.Contains("rules"))
                                {
                                    numberOfRules = br.DocumentText.Split(new char[] { '(', ')' })[1];
                                }
                                r.Message = String.Format("SUCCESS: {0} - {1}", br.Url.ToString(), numberOfRules);
                                r.Type = AuthResultType.PASS;
                            }

                        }
                        else
                        {
                            // Relogin button is present
                            r.Message = String.Format("FAILED: {0} - {1}.", br.Url.ToString(), br.Document.Title);
                            r.Type = AuthResultType.FAIL;
                        }

                        /*                
                            *  Windows Forms are not designed to work across different threads. 
                            *  Invoke method will run the delegate you pass to it in the UI thread.
                            *  If you want to manipulate UI elements via other threads, you'll have to run the actual manipulation on the UI thread. 
                            *  A control's InvokeRequired property will also tell you if you need to use Invoke rather than calling the method directly.
                           */
                        Invoke(new Action(() => DisplayResult(r)));
                        br.Dispose();
                    }
                }
            }
        }

        private void btnXMlFile_Click(object sender, EventArgs e)
        {
            ofdXMLfile.ShowDialog();

            if (!String.IsNullOrWhiteSpace(ofdXMLfile.FileName))
                txtXMLFile.Text = ofdXMLfile.FileName;
        }

        private void InitControls()
        {
            txtXMLFile.Enabled = false;

            String lastselectedxmlfile = XMLFileManager.GetFirewallsFile();

            if (!String.IsNullOrWhiteSpace(lastselectedxmlfile))
            {
                txtXMLFile.Text = lastselectedxmlfile;
            }

            ofdXMLfile.DefaultExt = "*.xml";
            ofdXMLfile.CheckFileExists = true;
            ofdXMLfile.FileName = String.Empty;

            if (!String.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("USERPROFILE")))
            {
                ofdXMLfile.InitialDirectory = System.Environment.GetEnvironmentVariable("USERPROFILE");
            }
            else
            {
                ofdXMLfile.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            }

            InitResults();

            btnAuthenticate.Enabled = true;
            statusTimer.Start();
        }

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(txtXMLFile.Text))
            {
                MessageBox.Show("Please select an XML file with Firewall URLs!");
                txtXMLFile.Focus();
                return;
            }

            if (!File.Exists(txtXMLFile.Text))
            {
                MessageBox.Show(String.Format("The file '{0}' does not exist.  Please enter or select a valid file.", txtXMLFile.Text));
                txtXMLFile.Focus();
                return;
            }

            if (clbURLGroups.Items.Count == 0)
            {
                MessageBox.Show("Nothing has been loaded into URL Groups list!  Please try another xml file!");
                txtXMLFile.Focus();
                txtXMLFile.Text = String.Empty;
                return;
            }
            else if (clbURLGroups.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please check at least one item in the URL Groups list!");
                clbURLGroups.Focus();
                return;
            }

            if (String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a Username!");
                txtUsername.Focus();
                return;
            }

            if (String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a Password!");
                txtPassword.Focus();
                return;
            }

            _ignoreSslErrors = ignoreCertificateErrorsToolStripMenuItem.Checked;
            _passwordString = txtPassword.Text;
            if (mustEnterPasswordToolStripMenuItem.Checked)
            {
                txtPassword.Clear();
            }

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "CORP"))
            {
                // validate the credentials

                bool isValid = pc.ValidateCredentials(txtUsername.Text, _passwordString, ContextOptions.Negotiate);
                if (!isValid)
                {
                    MessageBox.Show("Credentials are invalid! Please check Username and Password.");
                    txtUsername.Focus();
                    return;
                }
            }

            // disable button
            btnAuthenticate.Enabled = false;
            _lastAuth = DateTime.Now;

            // kick off the authentication routine
            AuthenticateWithFirewalls();

            //TODO: instead of blocking the main thread, consider parallel parent-child task that manages the btnAuthenticate control instead
            // block main thread for 1 second then re-enable the button
            Thread.Sleep(2000);
            btnAuthenticate.Enabled = true;

            // upon successful auth, save the file name and selected groups to cfg file
            XMLFileManager.SaveFirewallsFile(txtXMLFile.Text);
            XMLFileManager.SaveSelectedGroups(clbURLGroups.CheckedItems.OfType<String>().ToList());
        }

        private void PopulateFirewallGroupNames(List<String> GroupNames)
        {
            try
            {
                InitFirewallGroups();

                if (GroupNames != null && GroupNames.Count > 0)
                {

                    // sort then load groupnames from xml file into checkedlistbox
                    GroupNames.Sort();
                    ((ListBox)clbURLGroups).DataSource = GroupNames;

                    // call cfg file and get selected groups
                    List<String> selectedGroups = XMLFileManager.GetSavedGroups();

                    if (selectedGroups != null && selectedGroups.Count > 0)
                    {
                        for (int i = 0; i <= (clbURLGroups.Items.Count - 1); i++)
                        {
                            if (selectedGroups.Contains(clbURLGroups.Items[i]))
                            {
                                clbURLGroups.SetItemCheckState(i, CheckState.Checked);
                                // if any of the groups have been previously selected, put the cursor in the password field for ultimate laziness!
                                txtPassword.Select();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Could not load Firewall groups - there may not be any.  Please select a different XML file!");
                    txtXMLFile.Text = String.Empty;
                    txtXMLFile.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Unable to populate Group names.\r\nMessage: {0}", ex.Message));
                txtXMLFile.Focus();
                txtXMLFile.Text = String.Empty;
                return;
            }
        }

        private void AuthenticateWithFirewalls()
        {
            txtResults.Text = "Authenticating with the selected firewalls...\r";
            txtResults.AppendText(Environment.NewLine);

            List<FirewallGroup> fwgrps = FirewallGroupManager.GetSelectedFirewallGroups(_fwgrps, clbURLGroups.CheckedItems);

            if (fwgrps != null)
            {
                foreach (FirewallGroup fwgrp in fwgrps)
                {
                    foreach (String url in fwgrp.URLs)
                    {
                        try
                        {
                            RunBrowser(url);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format("Unable to run authentication routine. \r\nMessage: {0}", ex.Message));
                            InitControls();
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Unable to run authentication routine.  Firewall groups could not be determined.");
                InitControls();
                return;
            }
        }

        private void RunBrowser(String URL)
        {
            var br = new WebBrowser();
            br.DocumentCompleted += browser_DocumentCompleted;
            br.Navigate(URL);
        }

        private void DisplayResult(AuthResult result)
        {
            if (result != null)
            {
                txtResults.SelectionColor = Color.Green;

                if (result.Type == AuthResultType.FAIL)
                    txtResults.SelectionColor = Color.Red;

                txtResults.AppendText(result.Message);
                txtResults.AppendText(Environment.NewLine);
            }
        }

        private void txtXMLFile_TextChanged(object sender, EventArgs e)
        {
            InitResults();
            InitFirewallGroups();

            if (String.IsNullOrWhiteSpace(txtXMLFile.Text))
            {
                MessageBox.Show("Please select an XML file with Firewall URLs!");
                txtXMLFile.Focus();
                return;
            }

            // set global list of firewall groups
            try
            {
                _fwgrps = FirewallGroupManager.GetFirewallGroups(txtXMLFile.Text);
                PopulateFirewallGroupNames(_fwgrps.Select(grp => grp.Key).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                txtXMLFile.Focus();
                return;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                String createdfile = XMLFileManager.CreateXMLFirewallFile();
                MessageBox.Show(String.Format("Created '{0}'", createdfile), "File Created");
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Could not create the file.  \n Error: {0}", ex.Message), "File Creation Error");
            }
        }

        private void clbURLGroups_MouseMove(object sender, MouseEventArgs e)
        {
            if (_ttIndex != clbURLGroups.IndexFromPoint(e.Location))
                ShowToolTip();
        }

        private void ShowToolTip()
        {
            _ttIndex = clbURLGroups.IndexFromPoint(clbURLGroups.PointToClient(MousePosition));

            if (_ttIndex > -1)
            {
                String grpname = clbURLGroups.Items[_ttIndex].ToString();
                StringBuilder sb = new StringBuilder();

                Point p = PointToClient(MousePosition);
                ttUrls.ToolTipTitle = String.Format("{0} Group URLs", grpname);
                //ttUrls.SetToolTip(clbURLGroups, clbURLGroups.Items[ttIndex].ToString());

                var urls = _fwgrps.Where(grp => grp.Key == grpname).SelectMany(grp => grp.Value).ToList();

                if (urls != null)
                {
                    foreach (var url in urls)
                    {
                        sb.AppendFormat("{0}\n", url.ToString());
                    }
                }
                ttUrls.SetToolTip(clbURLGroups, sb.ToString());

            }
        }

        private void InitFirewallGroups()
        {
            clbURLGroups.ClearSelected();
            ((ListBox)clbURLGroups).DataSource = null;
            clbURLGroups.Items.Clear();
        }

        private void InitResults()
        {
            txtResults.Clear();
            txtResults.SelectionColor = Color.Black;
            txtResults.AppendText("Authentication results will show here.");
        }

        private void InitUsername()
        {
            txtUsername.Text = Environment.UserName;
        }

        private void InitConfigFile()
        {
            XMLFileManager.CreateConfigXMLFile();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    this.ShowInTaskbar = true;
                    break;
                case FormWindowState.Minimized:
                    this.ShowInTaskbar = false;
                    break;
                case FormWindowState.Normal:
                    this.ShowInTaskbar = true;
                    break;
                default:
                    break;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == reauthMenuItem)
            {
                btnAuthenticate.PerformClick();
            }
        }

        private void customToolStripMenuItem_Clicked(object sender, EventArgs e)
        {
            if (sender == menuItemOneHour)
            {
                menuItemOneHour.Checked = (sender as ToolStripMenuItem).Checked;
                menuItemTwoHours.Checked = false;
                menuItemFourHours.Checked = false;

            }
            else if (sender == menuItemTwoHours)
            {
                menuItemOneHour.Checked = false;
                menuItemTwoHours.Checked = (sender as ToolStripMenuItem).Checked;
                menuItemFourHours.Checked = false;
            }
            else if (sender == menuItemFourHours)
            {
                menuItemOneHour.Checked = false;
                menuItemTwoHours.Checked = false;
                menuItemFourHours.Checked = (sender as ToolStripMenuItem).Checked;
            }
        }

        private void menuItemOneHour_CheckStateChanged(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
            {
                if (!authTimer.Enabled)
                {
                    authTimer.Start();
                }
                int tagOut = 0;
                if (int.TryParse((sender as ToolStripMenuItem).Tag.ToString(), out tagOut))
                {
                    if (tagOut > 0)
                    {
                        authTimer.Interval = (int)TimeSpan.FromHours(tagOut).TotalMilliseconds;
                    }
                }
            }
            else
            {
                if (authTimer.Enabled)
                {
                    authTimer.Stop();
                }
            }
        }

        private void authTimer_Tick(object sender, EventArgs e)
        {
            btnAuthenticate.PerformClick();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            authTimer.Dispose();
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            if (_lastAuth != null)
            {
                if ((_lastAuth == null || (DateTime.Now - _lastAuth).TotalSeconds >= 300) && notifyIcon1.Icon != _iconSysTrayBad)
                {
                    if (_iconSysTrayNotify != null)
                    {
                        notifyIcon1.Icon = _iconSysTrayNotify;
                        notifyIcon1.Text = "Firewall Authenticator - Authentication Time";
                    }
                }
                else if (notifyIcon1.Icon != _iconSysTrayBad)
                {
                    if (_iconSysTrayGood != null)
                    {
                        notifyIcon1.Icon = _iconSysTrayGood;
                        notifyIcon1.Text = "Firewall Authenticator";
                    }
                }
            }
        }
    }
}
