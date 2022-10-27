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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            this.cbSite.Items.Add(Define.NEXTVOZ_HOST);
            this.cbSite.Items.Add(Define.XAMVN_HOST);
            this.cbSite.SelectedIndex = 0;
        }

        private bool validation()
        {
            if (string.IsNullOrEmpty(rbCookies.Text))
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    MessageBox.Show("Nhập tải khoản.");
                    return false;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Nhập mật khẩu.");
                    return false;
                }
            }

            return true;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!validation())
                return;

            btnLogin.Enabled = false;
            try
            {
                AbstractXenForoClient client = null;
                if (this.cbSite.SelectedItem.ToString() == Define.NEXTVOZ_HOST)
                    client = new NextVozClient();
                else
                    client = new XamVnClient();

                if (string.IsNullOrEmpty(rbCookies.Text))
                {
                    await client.Login(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    await client.Login(rbCookies.Text);
                }

                Setting.Instance.AddCookie(this.cbSite.SelectedItem.ToString(), client.GetCookies());

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
    }
}
