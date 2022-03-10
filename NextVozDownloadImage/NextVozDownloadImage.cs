using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage
{
    public class NextVozDownloadImage
    {
        private ConcurrentQueue<string> _pages = new ConcurrentQueue<string>();
        private ConcurrentQueue<Tuple<string, string>> _images = new ConcurrentQueue<Tuple<string, string>>();
        private List<Task> _tasks = new List<Task>();
        private HashSet<int> _downloadedImage = new HashSet<int>();
        private NextVozClient _client;
        private ImageStore _store;

        private bool _isPause = false;
        private bool _isStop = false;
        private bool _isGetThreadsEnd = false;

        public bool IsDownloading { get; internal set; } = false;


        public delegate void OnDownloadFinish(object sender);
        public event OnDownloadFinish DownloadFinishEvent;

        public NextVozDownloadImage()
        {
        }

        public async Task<ThreadInfo> GetThreadInfo(string url)
        {
            var id = Helpers.NextVozRegex.GetThreadId(url);
            if (string.IsNullOrEmpty(id))
                id = url;

            var info = new ThreadInfo();
            info.ThreadId = id;
            info.Url = $"https://voz.vn/t/{id}";

            if (_client == null)
                _client = new NextVozClient(Setting.Instance.Cookies);

            var content = await _client.GetContent(info.Url);

            info.Name = Helpers.NextVozRegex.GetThreadName(content);
            info.Page = Helpers.NextVozRegex.GetTotalPage(content);

            return info;
        }

        private string getImageUrl(string url)
        {
            if (url.StartsWith("http"))
                return url.Replace("&amp;", "&");

            if (url.StartsWith("//"))
                return $"http:{url}".Replace("&amp;", "&");

            return $"https://voz.vn/{url.TrimStart('/')}";
        }

        private async Task getImagesUrl()
        {
            var tasks = new List<Task>();

         //   for (int i = 0; i < Math.Max(Setting.Instance.NumberThreads / 2, 1); i++)
            {
                var task = Task.Run(async () =>
                {
                    while (true)
                    {
                        if (_isPause)
                        {
                            await Task.Delay(500);
                            continue;
                        }

                        if (_pages.IsEmpty || _isStop)
                            break;

                        if (_pages.TryDequeue(out string page))
                        {
                            try
                            {
                                var content = await _client.GetContent(page);

                                var images = NextVozRegex.GetImages(content).Select(url => getImageUrl(url));
                                var attachments = NextVozRegex.GetAttachmentImages(content).Select(url => getImageUrl($"attachments/{url}"));
                                var linkExternal = NextVozRegex.GetImageInLinkExternal(content).Select(url => getImageUrl(url));

                                foreach (var item in images)
                                {
                                    _images.Enqueue(new Tuple<string, string>(page, item));
                                }

                                foreach (var item in attachments)
                                {
                                    _images.Enqueue(new Tuple<string, string>(page, item));
                                }

                                foreach (var item in linkExternal)
                                {
                                    _images.Enqueue(new Tuple<string, string>(page, item));
                                }
                            }
                            catch (Exception ex)
                            {
                                Status.Set(ex.Message);

                                System.Diagnostics.Debug.WriteLine(ex);
                            }
                        }

                        await Task.Delay(500);
                    }
                });

                await task;

                //tasks.Add(task);

                //await Task.Delay(500);
            }

            //Task.WaitAll(tasks.ToArray());

            _isGetThreadsEnd = true;
        }

        private async Task download()
        {
            await Task.Delay(5000);

            var tasks = new List<Task>();

            for (int i = 0; i < Setting.Instance.NumberThreads; i++)
            {
                var index = i;

                var task = Task.Run(async () =>
                {
                    while (true)
                    {
                        if (_isPause)
                        {
                            await Task.Delay(500);
                            continue;
                        }

                        if (_images.IsEmpty && _isGetThreadsEnd)
                            break;

                        if (_isStop)
                            break;

                        if (_images.TryDequeue(out Tuple<string, string> item))
                        {
                            var hashCode = item.Item2.GetHashCode();
                            if (this._downloadedImage.Contains(hashCode))
                                continue;

                            this._downloadedImage.Add(hashCode);

                            try
                            {
                                _store.Info.LastDownloadedPage = NextVozRegex.GetPage(item.Item1);

                                using (var downloader = new ImageDownloader(item.Item2, Setting.Instance.Cookies))
                                {

                                    //  var info = downloader.GetImageInfo();

                                    DownloadProcess.Update(index, new ImageInfo
                                    {
                                        Url = item.Item2,
                                    });

                                    downloader.ProgressChangedEventHandler += (sender, value) =>
                                    {
                                        DownloadProcess.UpdateProcess(index, value);
                                    };

                                    DownloadProcess.UpdateProcess(index, 0);

                                    var data = await downloader.DownloadAsync();

                                    if (Setting.Instance.IgnoreSmallImage)
                                    {
                                        using (var imgStream = new MemoryStream(data.Data))
                                        {
                                            using (var img = Image.FromStream(imgStream))
                                            {
                                                if (img.Width < 200 || img.Height < 200)
                                                    continue;
                                            }
                                        }
                                    }

                                    _store.Save(data.Data, data.GetFileName());

                                    Status.IncrementSize(data.Size);
                                    Status.IncrementDownload();
                                    Status.Update();
                                }
                            }
                            catch (Exception ex)
                            {
                                Status.Set(ex.Message);

                                System.Diagnostics.Debug.WriteLine(item.Item1 + " " + item.Item2 + " " + ex.Message);
                            }

                            DownloadProcess.UpdateProcess(index, 100);
                        }
                        else
                            await Task.Delay(500);
                    }

                    DownloadProcess.Update(index, new ImageInfo()
                    {
                        Url = "Xong",
                        Size = 0,
                        Extension = ""
                    });
                });

                tasks.Add(task);
                await Task.Delay(100);
            }

            Task.WaitAll(tasks.ToArray());

            _store.Info.TotalDownloadedImage = Status.TotalDownloadedImage;
            _store.Info.TotalDownloadedSize = Status.TotalDownloadedSize;
            _store.SaveInfo();

            IsDownloading = false;

            DownloadFinishEvent?.Invoke(this);
        }

        public async Task Start()
        {
            if (_isPause)
            {
                _isPause = false;

                return;
            }

            Status.Set("Đang tải về ...");



            IsDownloading = true;
            _isPause = false;
            _isStop = false;
            _isGetThreadsEnd = false;

            _client = new NextVozClient(Setting.Instance.Cookies);

            var info = await GetThreadInfo(Setting.Instance.Link);
            var from = Math.Max(Setting.Instance.FromPage, 1);
            var to = Math.Max(Setting.Instance.ToPage, 1);

            if (Setting.Instance.AllPage)
            {
                from = 1;
                to = info.Page;
            }

            for (int i = from; i <= to; i++)
            {
                this._pages.Enqueue($"https://voz.vn/t/{info.ThreadId}/page-{i}");
            }

            DownloadProcess.Create(Setting.Instance.NumberThreads);

            var dir = Setting.Instance.SavePath;
            if (Setting.Instance.CreateDirByThreadName)
            {
                dir = Path.Combine(Application.StartupPath, "Images", info.ThreadId.Trim('/'));
            }

            _store = new ImageStore(
                        dir,
                         Setting.Instance.SubDirectory,
                         Setting.Instance.MaxImageInSubDirectory,
                         new DownloadedInfo()
                         { });

            Status.Set(_store.Info.TotalDownloadedImage, _store.Info.TotalDownloadedSize);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var t1 = Task.Run(getImagesUrl);

            var t2 = Task.Run(download);
        }

        public void Stop()
        {
            this._isStop = true;

            Status.Set("Ngừng");
        }

        public void Pause()
        {
            this._isPause = true;

            Status.Set("Tạm ngừng");
        }
    }
}
