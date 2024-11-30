using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage
{
    public abstract class AbstractXenForoClient : IClient, IDisposable
    {
        private bool _disposed = false;
        protected HttpClient Client;

        public CookieContainer CookieContainer;
        protected string Cookie;
        protected bool UseCurl = false;

        public void Dispose()
        {
            dispose(true);

            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;

            if (disposing)
            {
                this.Client.Dispose();
                CookieContainer = null;
            }
        }

        ~AbstractXenForoClient()
        {
            dispose(false);
        }
        public AbstractXenForoClient(string cookie = null)
        {

            this.Cookie = cookie;
            CookieContainer = new CookieContainer();
            if (!string.IsNullOrEmpty(cookie))
                this.AddCookie(cookie);

            var handler = new HttpClientHandler() { CookieContainer = CookieContainer, UseCookies = true, SslProtocols = System.Security.Authentication.SslProtocols.Tls13 };
            Client = new HttpClient(handler, true);
            Client.DefaultRequestHeaders
                     .UserAgent
                     .TryParseAdd(this.UserAgent);
        }

        protected virtual string UserAgent { set; get; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 Edg/131.0.0.0";
        protected abstract string HomePage { set; get; }
        protected abstract string HostName { set; get; }
        protected abstract string LoginUrl { set; get; }

        public virtual async Task InitialAsync()
        {
            //this.UseCurl = true;

            //return;


            try
            {
                var rp = await this.Client.GetAsync(HomePage);
                rp.EnsureSuccessStatusCode(); 

                return;
            }
            catch { }

            try
            {

                var rp = CurlWrapper.GetString(HomePage, this.UserAgent); 

                if (!string.IsNullOrEmpty(rp))
                    UseCurl = true;
            }
            catch { }
        }

        public virtual void AddCookie(CookieContainer cookieContainer)
        {
            var cookies = cookieContainer.GetCookies(new Uri(this.HomePage));
            this.CookieContainer.Add(cookies);
        }
        public virtual void AddCookie(string cookie)
        {
            this.AddCookie(Cookies.ParseCookies(cookie, this.HostName));
        }

        public virtual async Task<string> GetContent(string url)
        {
            //try
            //{
            //    var request = WebRequest.Create(url) as HttpWebRequest;
            //    request.Method = "GET";
            //    request.UserAgent = UserAgent;
            //    request.Host = this.HostName;
            //    request.Accept = "*/*";
            //    request.Headers.Add("Cookie", this.Cookie);

            //    var response = (await request.GetResponseAsync()) as HttpWebResponse;                   

            //    using (var reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        var s = await reader.ReadToEndAsync();

            //        return s;
            //    }
            //}
            //catch (WebException ex)
            //{
            //    using (var stream = ex.Response.GetResponseStream())
            //    using (var reader = new StreamReader(stream))
            //    {
            //        var s = await reader.ReadToEndAsync();

            //        return s;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            if (UseCurl)
            {
                var cookiesString = GetCookies();
                List<string> cookies = new List<string>();
                if (!string.IsNullOrEmpty(cookiesString))
                    cookies.Add($"Cookie: {cookiesString}");

                return CurlWrapper.GetString(url, UserAgent, cookies);
            }

            return await Client.GetStringAsync(url);
        }

        protected virtual async Task<string> GetCsrf()
        {
            var html = await Client.GetStringAsync(this.HomePage);

            return NextVozRegex.GetToken(html);
        }

        public virtual bool IsLogin(CookieContainer cookieContainer = null)
        {
            return (cookieContainer ?? this.CookieContainer)
                 .GetCookies(new Uri(this.HomePage))
                 .Cast<Cookie>()
                 .FirstOrDefault(item => item.Name == "xf_user") != null;

        }

        public string GetCookies()
        {
            var cookies = CookieContainer
                  .GetCookies(new Uri(this.HomePage))
                  .Cast<Cookie>().Select(item => item.Name + "=" + item.Value);

            return string.Join(";", cookies);
        }

        public virtual async Task Login(string userName, string password)
        {
            //   var response = await _client.PostAsync(url, data);

            var token = await GetCsrf();

            var content = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("login", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("remember", "1"),
                new KeyValuePair<string, string>("_xfRedirect", this.HomePage),
                new KeyValuePair<string, string>("_xfToken", token),
            });

            var response = await Client.PostAsync(this.LoginUrl, content);

            if (!IsLogin())
            {

                var html = await response.Content.ReadAsStringAsync();

                if (html.Contains("data-xf-init=\"re-captcha\""))
                {
                    new FormLoginBrowser(this.LoginUrl).ShowDialog();

                    var cookieContainer = Cookies.GetUriCookieContainer(new Uri(this.HomePage));

                    if (IsLogin(cookieContainer))
                    {
                        AddCookie(cookieContainer);

                        return;
                    }
                    else
                    {
                        throw new Exception("Đăng nhập thất bại, vui lòng thử lại");
                    }
                }

                throw new Exception("Sai mật khẩu, vui lòng thử lại");
            }
        }
        public virtual async Task Login(string cookies)
        {
            var cookieContainer = Cookies.ParseCookies(cookies, this.HostName);

            AddCookie(cookieContainer);

            await GetContent(this.HomePage);

            var isLogin = IsLogin();
            if (!isLogin)
            {
                throw new Exception("Cookie đã hết hạn");
            }
        }

        private static string getImageExtension(Image image)
        {
            if (image.RawFormat.Equals(ImageFormat.Jpeg))
                return "jpg";
            if (image.RawFormat.Equals(ImageFormat.Png))
                return "png";
            if (image.RawFormat.Equals(ImageFormat.Gif))
                return "gif";
            if (image.RawFormat.Equals(ImageFormat.Bmp))
                return "bmp";
            if (image.RawFormat.Equals(ImageFormat.Tiff))
                return "tiff";
            if (image.RawFormat.Equals(ImageFormat.Icon))
                return "ico";

            // Fallback for unknown formats
            return "unknown";
        }

        public async Task<ImageInfo> DownloadImageAsync(string url, Action<long, long> downloadProgressCallback)
        {
            var imageInfo = new ImageInfo();
            imageInfo.Url = url;
            Uri uri = new Uri(url);
            imageInfo.Name = System.IO.Path.GetFileNameWithoutExtension(uri.AbsolutePath).Split('.').FirstOrDefault();

            if (UseCurl)
            {
                var cookiesString = GetCookies();
                List<string> cookies = new List<string>();
                if (!string.IsNullOrEmpty(cookiesString))
                    cookies.Add($"Cookie: {cookiesString}");

                imageInfo.Data = CurlWrapper.Download(url, UserAgent, cookies);
                imageInfo.Size = imageInfo.Data.Length;
                using (var imgStream = new MemoryStream(imageInfo.Data))
                {
                    using (var img = Image.FromStream(imgStream))
                    {
                        imageInfo.Extension = getImageExtension(img);
                    }
                }

                downloadProgressCallback?.Invoke(imageInfo.Size, imageInfo.Size);

                return imageInfo;
            }

            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = response.Content;
            var totalBytes = response.Content.Headers.ContentLength ?? -1;
            imageInfo.Size = (int)responseContent.Headers?.ContentLength;
            imageInfo.Extension = responseContent.Headers.ContentType?.MediaType.Replace("image/", "").ToLower();
            imageInfo.Name = responseContent.Headers.ContentDisposition?.FileName?.Trim('"') ?? imageInfo.Name;

            using (var memoryStream = new MemoryStream())
            {
                using (var responseStream = await responseContent.ReadAsStreamAsync())
                {
                    byte[] buffer = new byte[8192];
                    long totalBytesRead = 0;
                    int bytesRead;

                    while ((bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);
                        totalBytesRead += bytesRead;
                        downloadProgressCallback?.Invoke(totalBytesRead, totalBytes);
                    }
                }

                imageInfo.Data = memoryStream.ToArray();
            }

            return imageInfo;
        }
    }
}
