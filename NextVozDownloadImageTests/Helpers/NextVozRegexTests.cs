using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NextVozDownloadImage.Helpers.Tests
{
    [TestClass()]
    public class NextVozRegexTests
    {
        const string LINK = "https://voz.vn/t/no-sex-vitamin-gai-xinh-moi-ngay-cho-đoi-mat-sang-khoe-đep.783806";
        const string THREAD_ID = "no-sex-vitamin-gai-xinh-moi-ngay-cho-đoi-mat-sang-khoe-đep.783806";

        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 Edg/131.0.0.0";

        private static string initContent = "";
        private static NextVozClient client;

        static NextVozRegexTests()
        {
            client = new NextVozClient(Setting.Instance.Cookies);

            initContent = getContent(LINK);
        }

        private static string getContent(string link)
        {
            return client.GetContent(link).Result;
        }

        [TestMethod()]
        public void GetThreadNameTest()
        { 
            var name = Helpers.NextVozRegex.GetThreadName(initContent);

            Assert.AreEqual(name, "[No Sex] Vitamin gái xinh mỗi ngày cho đôi mắt sáng khỏe đẹp");
        }

        [TestMethod()]
        public void GetThreadIdTest()
        {  
            Assert.AreEqual(NextVozRegex.GetThreadId($"{LINK}/"), THREAD_ID);
            Assert.AreEqual(NextVozRegex.GetThreadId($"{LINK}"), THREAD_ID);
            Assert.AreEqual(NextVozRegex.GetThreadId($"{LINK}/page-1"), THREAD_ID);
            Assert.AreEqual(NextVozRegex.GetThreadId($"{LINK}/page-1#post-1234"), THREAD_ID);
            Assert.AreNotEqual(NextVozRegex.GetThreadId($"{LINK}/page-1#post-1234"), THREAD_ID);
        }

        [TestMethod()]
        public void GetTotalPageTest()
        {
            var page = Helpers.NextVozRegex.GetTotalPage(initContent);

            Assert.AreEqual(page, 253);
        }

        [TestMethod()]
        public void GetImageTest()
        {
            var totalImage = Helpers.NextVozRegex.GetImages(initContent).Count();

            Assert.AreEqual(totalImage, 37);
        }
        [TestMethod()]
        public void GetImageAttachmentTest()
        {
            var totalImage = Helpers.NextVozRegex.GetAttachmentImages(initContent).Count();

            Assert.AreEqual(totalImage, 1);
        }

    }
}