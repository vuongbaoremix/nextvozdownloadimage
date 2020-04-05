using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextVozDownloadImage.Helpers.Tests
{
    [TestClass()]
    public class NextVozClientTests
    {
        [TestMethod()]
        public async Task LoginTest()
        {
            var nextvozClient = new NextVozClient();

            string userName = "";
            string password = "";

            await nextvozClient.Login(userName, password);

            Assert.IsTrue(true);
        }
    }
}