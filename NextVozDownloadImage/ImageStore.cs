using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; 
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage
{
    public class ImageStore
    {
        private string _dir;
        private bool _subDir;
        private int _maxImageInDir;

        public DownloadedInfo Info;
        private string _infoPath;
        private int _currentImageInDir = 0;
        private string _currentPath = "";
        private object _objLock = new object();
        private object _objLockInfo = new object();
         
        public ImageStore(string dir, bool subDir, int maxImageInDir, DownloadedInfo downloadedInfo)
        {
            _dir = dir;
            _subDir = subDir;
            _maxImageInDir = maxImageInDir;
            _infoPath = Path.Combine(dir, "info.json");

            var info = loadInfo();

            if (info == null)
                Info = downloadedInfo;
            else
            {
                if (info.Url != downloadedInfo.Url)
                {
                    Info = downloadedInfo;
                    Info.LastSubDir = info.LastSubDir;
                }
                else
                {
                    Info = info;
                }
            }
        }

        private DownloadedInfo loadInfo()
        {
            try
            {
                if (File.Exists(this._infoPath))
                {
                    return JsonConvert.DeserializeObject<DownloadedInfo>(this._infoPath);
                }
            }
            catch
            {

            }

            return null;
        }

        private void createSubDir()
        {
            while (true)
            {
                var path = Path.Combine(this._dir, this.Info.LastSubDir.ToString());

                if (Directory.Exists(path))
                {
                    var totalImage = Directory.GetFiles(path).Length;

                    if (totalImage >= this._maxImageInDir)
                    {
                        this.Info.LastSubDir++;
                        continue;
                    }

                    this._currentPath = path;
                    this._currentImageInDir = totalImage;

                    break;
                }
                else
                {
                    Directory.CreateDirectory(path);
                    this._currentPath = path;

                    break;
                }
            }
        }

        private string getSaveDir()
        {

            if (this._subDir)
            {
                lock (_objLock)
                {
                    if (string.IsNullOrEmpty(_currentPath))
                    {
                        createSubDir();
                    }
                    else
                    {
                        if (this._currentImageInDir >= this._maxImageInDir)
                        {
                            this.Info.LastSubDir++;
                            this._currentImageInDir = 0;

                            createSubDir();
                        }
                    }

                    return this._currentPath;
                }
            }

            return this._dir;
        }

        public void SaveInfo()
        {
            lock (_objLockInfo)
            {
                System.IO.File.WriteAllText(_infoPath, JsonConvert.SerializeObject(this.Info));
            }
        }

        public void Save(byte[] data, string fileName)
        {
            System.IO.File.WriteAllBytes(Path.Combine(getSaveDir(), fileName), data);

            Interlocked.Increment(ref this._currentImageInDir);
        }
    }
}
