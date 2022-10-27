using System.Text;

namespace NextVozDownloadImage
{
    public class NextVozClient : AbstractXenForoClient
    {
        protected override string HomePage { set; get; } = $"https://{Define.NEXTVOZ_HOST}/";
        protected override string HostName { set; get; } = $"{Define.NEXTVOZ_HOST}";
        protected override string LoginUrl { set; get; } = $"https://{Define.NEXTVOZ_HOST}/login/login";

        public NextVozClient(string cookie = "") : base(cookie)
        {
           
        }
    }
}
