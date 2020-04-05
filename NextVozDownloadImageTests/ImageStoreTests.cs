using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextVozDownloadImage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage.Tests
{
    [TestClass()]
    public class ImageStoreTests
    {


        [TestMethod()]
        public void SaveTest()
        {
            var path = System.IO.Path.Combine(Application.StartupPath, "temp");
            if (Directory.Exists(path))
                Directory.Delete(path, true);

            Directory.CreateDirectory(path);

            ImageStore store = new ImageStore(path, true, 10, new DownloadedInfo()
            {
                LastDownloadedPage = 1,
                LastSubDir = 1,
                ThreadId = "aaa",
                ThreadName = "aaaaaaaaaa",
                TotalDownloadedImage = 1,
                TotalDownloadedSize = 1,
                Url = "next.voz.vn"
            });

            for (int i = 0; i < 1000; i++)
            {
                store.Save(new byte[1024], $"{i}.bin");
            }

            store.SaveInfo();
        }
    }
}