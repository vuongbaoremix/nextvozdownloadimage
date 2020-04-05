using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DownloadImage.Helpers
{
   public static class RegexHelper
    {
        public static IEnumerable<string> Select(this MatchCollection matchCollection, Func<Match, string> func)
        {
            foreach (Match m in matchCollection)
            {
                yield return func(m);
            }
        }
    }
}
