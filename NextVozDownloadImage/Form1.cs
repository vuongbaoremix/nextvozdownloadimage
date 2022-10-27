using EXControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage
{
    public partial class Form1 : Form
    {
        private const string BUTTON_PAUSE_TEXT = "Tạm ngừng";
        private const string BUTTON_STOP_TEXT = "Ngừng";
        private Downloader _downloader = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void init()
        {
            System.Net.ServicePointManager.MaxServicePoints = Int32.MaxValue;
            System.Net.ServicePointManager.DefaultConnectionLimit = Int32.MaxValue;
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            try
            {
                txtLink.Text = Setting.Instance.Link;
                txtSavePath.Text = Setting.Instance.SavePath;
                cbDownloadAllPage.Checked = Setting.Instance.AllPage;
                cbSubDirectory.Checked = Setting.Instance.SubDirectory;
                cbIgnoreSmallImage.Checked = Setting.Instance.IgnoreSmallImage;
                cbCreateDirByThreadName.Checked = Setting.Instance.CreateDirByThreadName;
                nrMaxImageInSubDirectory.Value = Setting.Instance.MaxImageInSubDirectory;
                nrNumberThreads.Value = Setting.Instance.NumberThreads;
                nrFormPage.Value = Setting.Instance.FromPage;
                nrToPage.Value = Setting.Instance.ToPage;
            }
            catch
            {

            }

            if (string.IsNullOrEmpty(txtSavePath.Text))
            {
                txtSavePath.Text = Path.Combine(Application.StartupPath, "Images");
            }

            try
            {
                if (!string.IsNullOrEmpty(Setting.Instance.Link))
                {
                    var uri = new Uri(Setting.Instance.Link);

                    if (btnLogin.Text == "Đăng xuất")
                    {
                        Setting.Instance.CookiesDomain.Remove(uri.Host);
                        Setting.Save();

                        btnLogin.Text = "Đăng nhập"; ;

                        return;
                    } 

                    if (!string.IsNullOrEmpty(Setting.Instance.GetCookie(uri.Host)))
                        btnLogin.Text = "Đăng xuất";

                    Setting.Save();
                }
            }
            catch (Exception ex)
            { 
            }
 

            _downloader = new Downloader();

            if (!string.IsNullOrEmpty(Setting.Instance.Link))
                linkChanged();

            SetDoubleBuffered(this.lvDownloadProcess);

            EXListView lv = new EXListView();
            lv.Location = lvDownloadProcess.Location;
            lv.Width = lvDownloadProcess.Width;
            lv.Height = lvDownloadProcess.Height;
            lv.Anchor = lvDownloadProcess.Anchor;

            this.Controls.Remove(lvDownloadProcess);
            this.Controls.Add(lv);

            lv.Columns.Add("Link", 450);
            lv.Columns.Add("Process", 300);


            Status.Init(lbStatus, lbTotalDownloaded, lbTotalSize);
            DownloadProcess.Init(lv);
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }
        private void btnSelectSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            folderBrowser.SelectedPath = txtSavePath.Text;
            var rs = folderBrowser.ShowDialog(this);

            if (rs == DialogResult.OK)
                txtSavePath.Text = folderBrowser.SelectedPath;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            nrMaxImageInSubDirectory.Enabled = cbSubDirectory.Checked;

            Setting.Instance.SubDirectory = cbSubDirectory.Checked;
        }

        private void cbDownloadAllPage_CheckedChanged(object sender, EventArgs e)
        {
            nrFormPage.Enabled = nrToPage.Enabled = !cbDownloadAllPage.Checked;

            Setting.Instance.AllPage = cbDownloadAllPage.Checked;
        }

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == BUTTON_PAUSE_TEXT)
            {
                pause();
            }
            else
            {
                stop();
            }
        }

        private void start()
        {
            this.Invoke((Action)(() =>
            {
                btnStartDownload.Enabled = false;
                btnPause.Enabled = true;
                grOptions.Enabled = false;
                txtLink.Enabled = false;
            }));

            Setting.Save();

            if (_downloader == null || !_downloader.IsDownloading)
            {
                _downloader = new Downloader();

                _downloader.DownloadFinishEvent += (sender) =>
                {
                    stop();
                };
            }

            var t = _downloader.Start();
        }

        private void stop()
        {
            this.Invoke((Action)(() =>
            {
                btnStartDownload.Enabled = true;
                btnPause.Enabled = false;
                btnPause.Text = BUTTON_PAUSE_TEXT;
                grOptions.Enabled = true;
                txtLink.Enabled = true;
            }));

            _downloader.Stop();
        }

        private void pause()
        {
            this.Invoke((Action)(() =>
            {
                btnStartDownload.Enabled = true;
                btnPause.Enabled = true;
                btnPause.Text = BUTTON_STOP_TEXT;
                grOptions.Enabled = false;
            }));

            _downloader.Pause();
        }


        private CancellationTokenSource _cancellationTokenTextChanged;
        private async Task linkChanged()
        {
            var token = _cancellationTokenTextChanged.Token;

            await Task.Delay(500, token);
            if (token.IsCancellationRequested)
                return;
            try
            {
                Status.Set("Đang lấy thông tin.");

                var info = await _downloader.GetThreadInfo(Setting.Instance.Link);

                if (token.IsCancellationRequested)
                    return;

                this.Invoke((Action)(() =>
                {
                    nrToPage.Maximum = info.Page;
                    nrFormPage.Maximum = info.Page;
                    nrToPage.Value = info.Page;

                    nrFormPage.Value = 1;
                    btnStartDownload.Enabled = true;
                    lbTotalPage.Text = $"/{info.Page}";
                    this.Text = $"NextVoz Download Image - {info.Name} - Page: {info.Page}";
                }));

                Status.Set($"Thread: {info.Name} - Page: {info.Page}");
            }
            catch (Exception ex)
            {
                Status.Set(ex.Message);
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Setting.Save();
        }

        private void txtLink_TextChanged(object sender, EventArgs e)
        {
            Setting.Instance.Link = txtLink.Text;

            _cancellationTokenTextChanged?.Cancel();
            _cancellationTokenTextChanged = new CancellationTokenSource();

            Task.Run(linkChanged, _cancellationTokenTextChanged.Token);
        }

        private void nrFormPage_ValueChanged(object sender, EventArgs e)
        {
            if (nrFormPage.Value > nrToPage.Value)
            {
                nrFormPage.Value = nrToPage.Value;
                return;
            }

            Setting.Instance.FromPage = (int)nrFormPage.Value;
        }

        private void nrToPage_ValueChanged(object sender, EventArgs e)
        {
            if (nrFormPage.Value > nrToPage.Value)
            {
                nrFormPage.Value = nrToPage.Value;
                return;
            }

            Setting.Instance.ToPage = (int)nrToPage.Value;
        }

        private void txtSavePath_TextChanged(object sender, EventArgs e)
        {
            Setting.Instance.SavePath = txtSavePath.Text;
        }

        private void grOptions_Enter(object sender, EventArgs e)
        {

        }

        private void nrMaxImageInSubDirectory_ValueChanged(object sender, EventArgs e)
        {
            Setting.Instance.MaxImageInSubDirectory = (int)nrMaxImageInSubDirectory.Value;
        }

        private void nrNumberThreads_ValueChanged(object sender, EventArgs e)
        {
            Setting.Instance.NumberThreads = (int)nrNumberThreads.Value;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Setting.Instance.Link))
                {
                    var uri = new Uri(Setting.Instance.Link);

                    if (btnLogin.Text == "Đăng xuất")
                    {
                        Setting.Instance.CookiesDomain.Remove(uri.Host);
                        Setting.Save();

                        btnLogin.Text = "Đăng nhập"; ;

                        return;
                    }

                    new FormLogin().ShowDialog();

                    if (!string.IsNullOrEmpty(Setting.Instance.GetCookie(uri.Host)))
                    {
                        btnLogin.Text = "Đăng xuất";
                         
                        _downloader = new Downloader();
                        this.linkChanged();
                    }
                    Setting.Save();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init();
        }

        private void cbIgnoreSmallImage_CheckedChanged(object sender, EventArgs e)
        {
            Setting.Instance.IgnoreSmallImage = cbIgnoreSmallImage.Checked;
        }

        private void cbCreateDirByThreadName_CheckedChanged(object sender, EventArgs e)
        {
            Setting.Instance.CreateDirByThreadName = cbCreateDirByThreadName.Checked;

            btnSelectSavePath.Enabled = txtSavePath.Enabled = !cbCreateDirByThreadName.Checked;

            //  createStore();
        }
    }
}
