using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace NextVozDownloadImage
{
    public class ImageDownloader : IDisposable
    {
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36 Edg/99.0.1150.36";

        private string _cookies = "";
        private string _domain = "";
        private string _url = "";


        public delegate void ProcessChanged(object sender, int value);
        public event ProcessChanged ProgressChangedEventHandler;
        public byte[] Data;

        public IClient Client = null;

        public ImageDownloader(string url, string cookies = "", string domain = "")
        {
            this._url = url;
            this._cookies = cookies;
            this._domain = domain;
        }

        public async Task<ImageInfo> DownloadAsync()
        {
            Data = null;


            //CurlWrapper Curl = new CurlWrapper();
            //Data = Curl.Download(this._url, USER_AGENT);
            //{
            //    var imageInfo = new ImageInfo();
            //    Uri uri = new Uri(this._url);

            //    imageInfo.Url = this._url;
            //    imageInfo.Size = Data.Length;
            //    imageInfo.Extension = "jpg";
            //    imageInfo.Data = Data;
            //    imageInfo.Name = System.IO.Path.GetFileNameWithoutExtension(uri.AbsolutePath).Split('.').FirstOrDefault();

            //    return imageInfo;
            //}

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add(HttpRequestHeader.Cookie, this._cookies);
                webClient.Headers.Add(HttpRequestHeader.UserAgent, USER_AGENT);

                webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
                webClient.DownloadDataCompleted += webClient_DownloadDataCompleted;

                var data = await webClient.DownloadDataTaskAsync(this._url);

                var contentType = webClient.ResponseHeaders["Content-Type"];
                var contentDisposition = webClient.ResponseHeaders["Content-Disposition"];

                if (!contentType.Contains("image"))
                    throw new Exception("Invalid image format.");
                if (data == null || data.Length == 0)
                    throw new Exception("Download image info error");

                var imageInfo = new ImageInfo();

                imageInfo.Url = this._url;
                imageInfo.Size = data.Length;
                imageInfo.Extension = contentType.Replace("image/", "").ToLower();
                imageInfo.Data = data;

                if (!string.IsNullOrEmpty(contentDisposition) && contentDisposition.Contains("filename"))
                {
                    imageInfo.Name = Regex.Match(contentDisposition, @"filename=""(.*)\.(.*)""").Groups[1].Value;
                }

                if (string.IsNullOrEmpty(imageInfo.Name))
                {
                    Uri uri = new Uri(this._url);
                    imageInfo.Name = System.IO.Path.GetFileNameWithoutExtension(uri.AbsolutePath).Split('.').FirstOrDefault();
                }

                return imageInfo;
            }
        }

        void webClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            this.ProgressChangedEventHandler?.Invoke(this, 100);
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.ProgressChangedEventHandler?.Invoke(this, e.ProgressPercentage);
        }

        public ImageInfo GetImageInfo()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(this._url);
            req.Method = "HEAD";
            req.UserAgent = USER_AGENT;
            if (!string.IsNullOrEmpty(this._cookies))
                req.CookieContainer = Cookies.ParseCookies(this._cookies, this._domain);

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                if (!resp.ContentType.Contains("image"))
                    throw new Exception("Is not image format.");

                if (resp.ContentLength < 0)
                    throw new Exception("Get image info error");

                var imageInfo = new ImageInfo();

                imageInfo.Url = this._url;
                imageInfo.Size = resp.ContentLength;
                imageInfo.Extension = resp.ContentType.Replace("image/", "").ToLower();

                var contentDisposition = resp.Headers.GetValues("content-disposition");
                if (contentDisposition != null)
                {
                    try
                    {
                        foreach (var item in contentDisposition)
                        {
                            if (item.Contains("filename"))
                            {
                                imageInfo.Name = Regex.Match(item, @"filename=""(.*)\.(.*)""").Groups[1].Value;
                                break;
                            }
                        }
                    }
                    catch { }
                }

                if (string.IsNullOrEmpty(imageInfo.Name))
                {
                    Uri uri = new Uri(this._url);
                    imageInfo.Name = System.IO.Path.GetFileNameWithoutExtension(uri.AbsolutePath).Split('.').FirstOrDefault();
                }

                return imageInfo;
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
                Data = null;
                ProgressChangedEventHandler = null;
                _cookies = null;
            }
        }

        ~ImageDownloader()
        {
            dispose(false);
        }
    }
}
