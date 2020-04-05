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
        const string LINK = "https://next.voz.vn/t/no-sex-ao-dai.14257/";
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";

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

            Assert.AreEqual(name, "[No Sex] Áo dài");
        }

        [TestMethod()]
        public void GetThreadIdTest()
        {  
            Assert.AreEqual(NextVozRegex.GetThreadId("https://next.voz.vn/t/no-sex-ao-dai.14257/"), "no-sex-ao-dai.14257");
            Assert.AreEqual(NextVozRegex.GetThreadId("https://next.voz.vn/t/no-sex-ao-dai.14257"), "no-sex-ao-dai.14257");
            Assert.AreEqual(NextVozRegex.GetThreadId("https://next.voz.vn/t/no-sex-ao-dai.14257/page-1"), "no-sex-ao-dai.14257");
            Assert.AreEqual(NextVozRegex.GetThreadId("https://next.voz.vn/t/no-sex-ao-dai.14257/page-1#post-1234"), "no-sex-ao-dai.14257");
            Assert.AreNotEqual(NextVozRegex.GetThreadId("https://next.voz.vn/no-sex-ao-dai.14257/page-1#post-1234"), "no-sex-ao-dai.14257");
        }

        [TestMethod()]
        public void GetTotalPageTest()
        {
            var page = Helpers.NextVozRegex.GetTotalPage(initContent);

            Assert.AreEqual(page, 8);
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