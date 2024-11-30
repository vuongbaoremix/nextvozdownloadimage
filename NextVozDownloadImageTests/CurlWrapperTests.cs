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
    public class CurlWrapperTests
    {
        const string Host = "https://voz.vn";

        [TestMethod()]
        public void GetStringTest()
        {
            var url = "https://voz.vn";
            var userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 Edg/131.0.0.0";

            var response = new CurlWrapper().GetString(url, userAgent);


            Assert.IsTrue(!string.IsNullOrEmpty(response));
        }
        [TestMethod()]
        public void GetStringWithoutUserAgentTest()
        {

            try
            {
                var response = new CurlWrapper().GetString(Host);
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

    }
}