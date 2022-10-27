using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage
{
    public partial class FormLoginBrowser : Form
    {
        string domain = "";

        public FormLoginBrowser(string domain)
        {
            InitializeComponent();

            this.domain = domain;   
            //var fm = new Form();
            var browser = new WebBrowser();  
            this.Controls.Add(browser);
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            browser.Navigate(this.domain);
            browser.Navigated += Browser_Navigated;
        }

        private void Browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Task.Run(() =>
            {
                var cookies = Cookies.GetUriCookieContainer(new Uri(this.domain));

                var client = new NextVozClient();

                if (client.IsLogin(cookies))
                {
                    try
                    {
                        this.Invoke((Action)(() => Close()));
                    }
                    catch { }
                }
            });

        }
    }
}
