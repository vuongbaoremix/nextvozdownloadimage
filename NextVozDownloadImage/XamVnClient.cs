using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace NextVozDownloadImage
{
    public class XamVnClient : AbstractXenForoClient
    {
        protected override string HomePage { set; get; } = $"https://{Define.XAMVN_HOST}/";
        protected override string HostName { set; get; } = $"{Define.XAMVN_HOST}";
        protected override string LoginUrl { set; get; } = $"https://{Define.XAMVN_HOST}/login/login";

        public XamVnClient(string cookie = ""): base(cookie)
        { 
        }

        public override bool IsLogin(CookieContainer cookieContainer = null)
        {
            return (cookieContainer ?? this.CookieContainer)
                 .GetCookies(new Uri(this.HomePage))
                 .Cast<Cookie>()
                 .FirstOrDefault(item => item.Name == "xfa_user") != null; 
        }
    }
}
