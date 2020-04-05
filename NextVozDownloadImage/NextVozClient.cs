using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NextVozDownloadImage
{
    public class NextVozClient : IDisposable
    {

        private HttpClient _client;

        private const string NEXT_VOZ_HOME = "https://next.voz.vn/";
        private const string NEXT_VOZ_HOST_NAME = "next.voz.vn";
        private const string LOGIN_URL = "https://next.voz.vn/login/login";
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";

        public CookieContainer CookieContainer;

        public NextVozClient(string cookie = "")
        {
            CookieContainer = Cookies.ParseCookies(cookie, NEXT_VOZ_HOST_NAME);

            var handler = new HttpClientHandler() { CookieContainer = CookieContainer, UseCookies = true };

            _client = new HttpClient(handler);
            _client.DefaultRequestHeaders
                     .UserAgent
                     .TryParseAdd(USER_AGENT);
        }


        public async Task<string> GetContent(string url)
        {
            return await _client.GetStringAsync(url);
        }

        private async Task<string> getCsrf()
        {
            var html = await GetContent(NEXT_VOZ_HOME);

            return NextVozRegex.GetToken(html);
        }

        public static bool IsLogin(CookieContainer cookieContainer)
        {
            return cookieContainer
                 .GetCookies(new Uri(NEXT_VOZ_HOME))
                 .Cast<Cookie>()
                 .FirstOrDefault(item => item.Name == "xf_user") != null;

        }

        private async Task<bool> IsLogin()
        {
            try
            {
                await GetContent(NEXT_VOZ_HOME);

                return IsLogin(this.CookieContainer);
            }
            catch
            {

            }

            return false;
        }

        private void addCookie(CookieContainer cookieContainer)
        {
            this.CookieContainer.Add(cookieContainer.GetCookies(new Uri(NEXT_VOZ_HOME)));
        }

        public async Task Login(string cookies)
        {
            var cookieContainer = Cookies.ParseCookies(cookies, NEXT_VOZ_HOST_NAME);

            addCookie(cookieContainer);

            var isLogin = await IsLogin();
            if (!isLogin)
            {
                throw new Exception("Cookie đã hết hạn");
            }
        }

        public string GetCookies()
        {
            var cookies = CookieContainer
                  .GetCookies(new Uri(NEXT_VOZ_HOME))
                  .Cast<Cookie>().Select(item => item.Name + "=" + item.Value);

            return string.Join(';', cookies);
        }

        public async Task Login(string userName, string password)
        {
            //   var response = await _client.PostAsync(url, data);

            var token = await getCsrf();

            var content = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("login", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("remember", "1"),
                new KeyValuePair<string, string>("_xfRedirect", NEXT_VOZ_HOME),
                new KeyValuePair<string, string>("_xfToken", token),
            });

            var response = await _client.PostAsync(LOGIN_URL, content);

            if (!IsLogin(this.CookieContainer))
            {

                var html = await response.Content.ReadAsStringAsync();

                if (html.Contains("data-xf-init=\"re-captcha\""))
                {
                    new FormLoginBrowser().ShowDialog();

                    var cookieContainer = Cookies.GetUriCookieContainer(new Uri(NEXT_VOZ_HOME));

                    if (IsLogin(cookieContainer))
                    {
                        addCookie(cookieContainer);

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

        private bool _disposed = false;
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
                this._client.Dispose();
                CookieContainer = null;
            }
        }

        ~NextVozClient()
        {
            dispose(false);
        }
    }
}
