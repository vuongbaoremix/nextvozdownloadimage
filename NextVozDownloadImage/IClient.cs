using System;
using System.Net;
using System.Threading.Tasks;

namespace NextVozDownloadImage
{
    public interface IClient
    {
        void AddCookie(string cookie);
        void AddCookie(CookieContainer cookieContainer);
        string GetCookies(); 
        Task<string> GetContent(string url); 
        Task Login(string cookies);
        Task Login(string userName, string password); 
        bool IsLogin(CookieContainer cookieContainer);
        Task InitialAsync();
        Task<ImageInfo> DownloadImageAsync(string url, Action<long, long> downloadProgressCallback);
    }
}
