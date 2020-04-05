using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextVozDownloadImage;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextVozDownloadImage.Tests
{
    [TestClass()]
    public class ImageInfoTests
    { 
        [TestMethod()]
        public void GetFileNameTest()
        {
            var fileName = new ImageInfo
            { 
                Name = "AAAA",
                Url = "next.voz.vn",
                Extension = "png",
                Data= new byte[10]
            }.GetFileName();

            Assert.IsTrue(fileName.Contains("AAAA"));
        }
    }
}