using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NextVozDownloadImage
{
    public class ImageInfo
    {
        public string Name { set; get; }
        public string Url { set; get; }
        public string Extension { set; get; }
        public long Size { set; get; }
        public byte[] Data { set; get; }



        public string GetFileName()
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            var buffer = md5.ComputeHash(Data);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in buffer)
            {
                stringBuilder.Append(b.ToString("x2"));
            }

            return $"{Name}_{stringBuilder.ToString()}.{Extension}";
        }
    }
}
