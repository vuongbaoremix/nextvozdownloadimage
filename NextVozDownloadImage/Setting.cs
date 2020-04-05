using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text; 

namespace NextVozDownloadImage
{
   public class Setting
    {
        const string SETTING_PATH = "setting.json";
        private static readonly Lazy<Setting> _lazy = new Lazy<Setting>(() => Load());

        public static Setting Instance { get => _lazy.Value; }

        public string Link { set; get; }
        public int FromPage { set; get; } = 1;
        public int ToPage { set; get; } = 1;
        public bool AllPage { set; get; } = true;
        public string SavePath { set; get; }
        public bool SubDirectory { set; get; } = false;
        public int MaxImageInSubDirectory { set; get; } = 5000;
        public int NumberThreads { set; get; } = 20;
        public string Cookies { set; get; }
        public bool IgnoreSmallImage { set; get; } = true;
        public bool CreateDirByThreadName { set; get; } = true;

        public static Setting Load()
        {
            try
            {
                if (System.IO.File.Exists(SETTING_PATH))
                {
                    var json = System.IO.File.ReadAllText(SETTING_PATH);

                    return JsonConvert.DeserializeObject<Setting>(json);
                }
            }
            catch
            {
            }

            return new Setting(); 
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(Instance);

            System.IO.File.WriteAllText(SETTING_PATH, json);
        }
    }
}
