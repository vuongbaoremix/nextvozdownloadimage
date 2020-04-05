using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextVozDownloadImage.Helpers
{
   public class HumanReadable
    {
       public static String BytesToString(long len)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; 

            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // Show a single decimal place, and no space.
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}
