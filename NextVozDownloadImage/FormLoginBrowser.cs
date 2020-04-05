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
        public FormLoginBrowser()
        {
            InitializeComponent();

            //var fm = new Form();
            var browser = new WebBrowser();  
            this.Controls.Add(browser);
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            browser.Navigate("https://next.voz.vn/login");
            browser.Navigated += Browser_Navigated;
        }

        private void Browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Task.Run(() =>
            {
                var cookies = Cookies.GetUriCookieContainer(new Uri("https://next.voz.vn"));
                if (NextVozClient.IsLogin(cookies))
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
