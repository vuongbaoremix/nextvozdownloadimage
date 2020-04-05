using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextVozDownloadImage
{
    public class DownloadedInfo
    {
        public string Url { set; get; } = "";
        public string ThreadName { set; get; } = "";
        public string ThreadId { set; get; } = "";
        public int LastDownloadedPage { set; get; } = 1;
        public int LastSubDir { set; get; } = 1;
        public int TotalDownloadedImage { set; get; } = 0;
        public long TotalDownloadedSize { set; get; } = 0;
    }
}
