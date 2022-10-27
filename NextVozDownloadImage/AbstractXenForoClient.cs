using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NextVozDownloadImage
{
    public abstract class AbstractXenForoClient : IClient, IDisposable
    {
        private bool _disposed = false;
        protected HttpClient Client;

        public CookieContainer CookieContainer;
        protected string Cookie;

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

            var handler = new HttpClientHandler() { CookieContainer = CookieContainer, UseCookies = true };
            Client = new HttpClient(handler, true);
            Client.DefaultRequestHeaders
                     .UserAgent
                     .TryParseAdd(this.UserAgent); 
        }

        protected virtual string UserAgent { set; get; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36 Edg/93.0.961.47";
        protected abstract string HomePage { set; get; }
        protected abstract string HostName { set; get; }
        protected abstract string LoginUrl { set; get; }


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
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.UserAgent = UserAgent;
                request.Host = this.HostName;
                request.Accept = "*/*";
                request.Headers.Add("Cookie", this.Cookie);

                var response = (await request.GetResponseAsync()) as HttpWebResponse;                   

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var s = await reader.ReadToEndAsync();

                    return s;
                }
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var s = await reader.ReadToEndAsync();

                    return s;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return await Client.GetStringAsync(url);
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

    }
}
