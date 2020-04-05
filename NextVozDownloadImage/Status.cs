using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage
{
    public static class Status
    {
        private static Label _status = null;
        private static Label _downloaded = null;
        private static Label _size = null;

        public static int TotalDownloadedImage = 0;
        public static long TotalDownloadedSize = 0;

        public static void Init(Label label, Label downloaded, Label size)
        {
            _status = label;
            _downloaded = downloaded;
            _size = size;
        }

        public static void Set(int downloaded, long size)
        {
            TotalDownloadedImage = downloaded;
            TotalDownloadedSize = size;

            Update();
        }

        public static void Set(string msg)
        {
            _status?.Invoke((Action)(() => _status.Text = msg));
        }

        public static void Reset()
        {
            _status?.Invoke((Action)(() => _status.Text = ""));
            _downloaded?.Invoke((Action)(() => _downloaded.Text = ""));
            _size?.Invoke((Action)(() => _size.Text = ""));
        }

        public static void Update()
        {
            _status?.Invoke((Action)(() => _status.Text = ""));
            _downloaded?.Invoke((Action)(() => _downloaded.Text = TotalDownloadedImage.ToString()));
            _size?.Invoke((Action)(() => _size.Text = Helpers.HumanReadable.BytesToString(TotalDownloadedSize)));
        }

        public static void IncrementSize(long size)
        {
            Interlocked.Add(ref TotalDownloadedSize, size);
        }

        public static void IncrementDownload()
        {
            Interlocked.Increment(ref TotalDownloadedImage);
        }
    }
}
