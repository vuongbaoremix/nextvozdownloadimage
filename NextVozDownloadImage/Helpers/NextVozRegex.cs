using DownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NextVozDownloadImage.Helpers
{

    public interface IParser
    {
        string GetThreadId(string url);
        int GetPage(string url);
        int GetTotalPage(string pageContent);
        string GetThreadName(string pageContent);
        //IEnumerable<string> GetAttachmentImages(string pageContent);
        IEnumerable<string> GetImages(string pageContent);
        //IEnumerable<string> GetImageInLinkExternal(string pageContent);
        string GetToken(string pageContent);

        string GetThreadUrl(string id, int page = 0);
    }

    public class NextVozParser : IParser
    {
        private const string REGEX_THREAD_NAME = @"<meta property=""og:title"" content=""(.*)"" \/>";
        private const string REGEX_PAGE = @"pageNav-page.*\/page-(.*)"">.*[^<]*<\/ul>";
        private const string REGEX_ATTACHMENT = @"<a.* href=""\/attachments\/(.*)\/"" target=""_blank""";
        private const string REGEX_IMAGE = @"class=""bbImageWrapper.*data-src=""([^""]+)""";
        private const string REGEX_LINK_EXTERNAL = @"<a href=.* class=""link link--external"".*\n.*\n.*<img src=""([^""]+)""";
        private const string REGEX_THREAD = @"voz.vn\/t\/([^\/]*)";
        private const string REGEX_Get_PAGE_FROM_URL = @"\/page-(.*)";
        private const string REGEX_GET_TOKEN = @"data-csrf=""(.*)""";

        public string GetThreadName(string html)
        {
            return Regex.Match(html, REGEX_THREAD_NAME, RegexOptions.Multiline).Groups[1].Value;
        }

        public int GetTotalPage(string html)
        {
            try
            {
                var page = Regex.Match(html, REGEX_PAGE, RegexOptions.Multiline).Groups[1].Value;

                return Int32.Parse(page);
            }
            catch
            {
                return 1;
            }
        }

        public string GetThreadId(string url)
        {
            return Regex.Match(url, REGEX_THREAD).Groups[1].Value;
        }

        public IEnumerable<string> GetAttachmentImages(string html)
        {
            var matches = Regex.Matches(html, REGEX_ATTACHMENT, RegexOptions.Multiline);

            return matches.Select(item => item.Groups[1].Value).Distinct();
        }


        private string formatUrl(string url)
        {
            if (url.StartsWith("http"))
                return url.Replace("&amp;", "&");

            if (url.StartsWith("//"))
                return $"http:{url}".Replace("&amp;", "&");

            return $"https://{Define.NEXTVOZ_HOST}/{url.TrimStart('/')}";
        }

        public IEnumerable<string> GetImages(string html)
        {
            List<string> images = Regex.Matches(html, REGEX_IMAGE, RegexOptions.Multiline).Select(item => item.Groups[1].Value).Distinct().Select(formatUrl).ToList();
            images.AddRange(GetAttachmentImages(html).Select(url => formatUrl($"attachments/{url}")));
            images.AddRange(GetImageInLinkExternal(html).Select(formatUrl));

            return images;
        }

        public IEnumerable<string> GetImageInLinkExternal(string html)
        {
            return Regex.Matches(html, REGEX_LINK_EXTERNAL, RegexOptions.Multiline).Select(item => item.Groups[1].Value).Distinct();
        }

        public string GetToken(string html)
        {
            return Regex.Match(html, REGEX_GET_TOKEN).Groups[1].Value;
        }

        public int GetPage(string url)
        {
            var page = Regex.Match(url, REGEX_Get_PAGE_FROM_URL, RegexOptions.Multiline).Groups[1].Value;

            return Int32.Parse(page);
        }

        public string GetThreadUrl(string id, int page = 0)
        {
            var url = $"https://{Define.NEXTVOZ_HOST}/t/{id}";
            if (page > 0)
                url = $"{url}/page-{page}";
            return url;
        }
    }

    public class XamVnParser : IParser
    {
        private const string REGEX_THREAD_NAME = @"<meta property=""og:title"" content=""(.*)"" \/>";
        private const string REGEX_PAGE = @"pageNav-page.*\/page-(.*)"">.*[^<]*<\/ul>";
        private const string REGEX_ATTACHMENT = @"<a.* href="".*\/attachments\/(.*)\/"" target=""_blank""";
        private const string REGEX_IMAGE = @"class=""bbImageWrapper.*data-src=""([^""]+)""";
        private const string REGEX_LINK_EXTERNAL = @"<a href=.* class=""link link--external"".*\n.*\n.*<img src=""([^""]+)""";
        private static readonly string REGEX_THREAD = $@"{Define.XAMVN_HOST}\/r\/([^\/]*)";
        private const string REGEX_Get_PAGE_FROM_URL = @"\/page-(.*)";
        private const string REGEX_GET_TOKEN = @"data-csrf=""(.*)""";

        public string GetThreadName(string html)
        {
            return Regex.Match(html, REGEX_THREAD_NAME, RegexOptions.Multiline).Groups[1].Value;
        }

        public int GetTotalPage(string html)
        {
            try
            {
                var page = Regex.Match(html, REGEX_PAGE, RegexOptions.Multiline).Groups[1].Value;

                return Int32.Parse(page);
            }
            catch
            {
                return 1;
            }
        }

        public string GetThreadId(string url)
        {
            return Regex.Match(url, REGEX_THREAD).Groups[1].Value;
        }

        public IEnumerable<string> GetAttachmentImages(string html)
        { 
            var urls = Regex.Matches(html, REGEX_ATTACHMENT, RegexOptions.Multiline).Select(item => item.Groups[1].Value).Distinct().ToList();

            return urls;
        }
        private string formatUrl(string url)
        {
            if (url.StartsWith("http"))
                return url.Replace("&amp;", "&");

            if (url.StartsWith("//"))
                return $"http:{url}".Replace("&amp;", "&");

            return $"https://{Define.XAMVN_HOST}/{url.TrimStart('/')}";
        }

        public IEnumerable<string> GetImages(string html)
        {
            List<string> images = Regex.Matches(html, REGEX_IMAGE, RegexOptions.Multiline).Select(item => item.Groups[1].Value).Distinct().Select(formatUrl).ToList();
            images.AddRange(GetAttachmentImages(html).Select(url => formatUrl($"attachments/{url}")));
            images.AddRange(GetImageInLinkExternal(html).Select(formatUrl));

            return images;
        }


        public IEnumerable<string> GetImageInLinkExternal(string html)
        {
            return Regex.Matches(html, REGEX_LINK_EXTERNAL, RegexOptions.Multiline).Select(item => item.Groups[1].Value).Distinct();
        }

        public string GetToken(string html)
        {
            return Regex.Match(html, REGEX_GET_TOKEN).Groups[1].Value;
        }

        public int GetPage(string url)
        {
            var page = Regex.Match(url, REGEX_Get_PAGE_FROM_URL, RegexOptions.Multiline).Groups[1].Value;

            return Int32.Parse(page);
        }

        public string GetThreadUrl(string id, int page = 0)
        {
            var url = $"https://{Define.XAMVN_HOST}/r/{id}";

            if (page > 0)
                url = $"{url}/page-{page}";

            return url;
        }
    }

    public static class NextVozRegex
    {

        private const string REGEX_THREAD_NAME = @"<meta property=""og:title"" content=""(.*)"" \/>";
        private const string REGEX_PAGE = @"pageNav-page.*\/page-(.*)"">.*[^<]*<\/ul>";
        private const string REGEX_ATTACHMENT = @"<a.* href=""\/attachments\/(.*)\/"" target=""_blank""";
        private const string REGEX_IMAGE = @"class=""bbImageWrapper.*data-src=""([^""]+)""";
        private const string REGEX_LINK_EXTERNAL = @"<a href=.* class=""link link--external"".*\n.*\n.*<img src=""([^""]+)""";
        private const string REGEX_THREAD = @"voz.vn\/t\/([^\/]*)";
        private const string REGEX_Get_PAGE_FROM_URL = @"\/page-(.*)";
        private const string REGEX_GET_TOKEN = @"data-csrf=""([^""]+)""";

        public static string GetThreadName(string html)
        {
            return Regex.Match(html, REGEX_THREAD_NAME, RegexOptions.Multiline).Groups[1].Value;
        }

        public static int GetTotalPage(string html)
        {
            try
            {
                var page = Regex.Match(html, REGEX_PAGE, RegexOptions.Multiline).Groups[1].Value;

                return Int32.Parse(page);
            }
            catch
            {
                return 1;
            }
        }

        public static string GetThreadId(string url)
        {
            return Regex.Match(url, REGEX_THREAD).Groups[1].Value;
        }

        public static IEnumerable<string> GetAttachmentImages(string html)
        {
            var matches = Regex.Matches(html, REGEX_ATTACHMENT, RegexOptions.Multiline);

            return matches.Select(item => item.Groups[1].Value).Distinct();
        }

        public static IEnumerable<string> GetImages(string html)
        {
            return Regex.Matches(html, REGEX_IMAGE, RegexOptions.Multiline).Select(item => item.Groups[1].Value).Distinct();
        }

        public static IEnumerable<string> GetImageInLinkExternal(string html)
        {
            return Regex.Matches(html, REGEX_LINK_EXTERNAL, RegexOptions.Multiline).Select(item => item.Groups[1].Value).Distinct();
        }

        public static string GetToken(string html)
        {
            return Regex.Match(html, REGEX_GET_TOKEN).Groups[1].Value;
        }

        public static int GetPage(string url)
        {
            var page = Regex.Match(url, REGEX_Get_PAGE_FROM_URL, RegexOptions.Multiline).Groups[1].Value;

            return Int32.Parse(page);
        }
    }

    public static class Parser
    {
        public static IParser GetParser(string url)
        {
            string pattern = @"(http:\/\/|https:\/\/)?([^\/]*)\/";
            RegexOptions options = RegexOptions.Multiline;

            var m = Regex.Match(url, pattern, options);

            if (m.Success && m.Groups[2].Value == Define.XAMVN_HOST)
                return new XamVnParser();

            return new NextVozParser();
        }
    }
}
