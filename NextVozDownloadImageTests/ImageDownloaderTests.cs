using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextVozDownloadImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextVozDownloadImage.Tests
{
    [TestClass()]
    public class ImageDownloaderTests
    {
        [TestMethod()]
        public void DownloadTest()
        {
            var cookies = Setting.Instance.Cookies;

            var downloader = new ImageDownloader("https://next.voz.vn/attachments/fb_img_1585840446117-jpg.22472/", cookies);

            var rs = downloader.DownloadAsync().Result;

            Assert.IsTrue(rs.Data.Length > 0);
        }

        [TestMethod()]
        public void GetImageInfoTest()
        {
            var cookies = Setting.Instance.Cookies;

            var downloader = new ImageDownloader("https://voz.vn/attachments/1587229700-930-c-ng-s-1587110439-width768height960-jpg.119370/", cookies);

            var rs = downloader.GetImageInfo();

            Assert.IsTrue(rs!=null);
        }
    }
}