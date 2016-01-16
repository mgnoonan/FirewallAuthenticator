using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirewallAuthenticator
{
    /// <summary>
    /// NOT USED - considering for parallel dev to block UI controls during async authentication
    /// </summary>
    public class FirewallClient
    {
        TaskCompletionSource<object> navigation;
        WebBrowser br;

        private AuthResult _result;
        public AuthResult Result
        {
            get { return _result; }
        }

        public FirewallClient(WebBrowser wb, String Username, String Password)
        {
            navigation = new TaskCompletionSource<object>();
            br = wb;
            br.DocumentCompleted += (sender, e) =>
            {
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
                                br.Document.GetElementById("DATA").SetAttribute("value", Username);
                                br.Document.GetElementsByTagName("INPUT")[3].InvokeMember("click");
                            }
                            // password page
                            else if (step == "2")
                            {
                                // fill in password and click submit button
                                br.Document.GetElementById("DATA").OuterHtml = String.Format("<INPUT value=\"{0}\" name=DATA>", Password);
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

                            br.Dispose();
                            _result = r;
                        }
                    }
                }
            };
        }

        public Task Authenticate(String url)
        {
            navigation = new TaskCompletionSource<object>();
            br.Navigate(url);
            return navigation.Task;
        }
    }
}
