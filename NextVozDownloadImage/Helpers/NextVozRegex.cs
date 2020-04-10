using DownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NextVozDownloadImage.Helpers
{
    public static class NextVozRegex
    {

        private const string REGEX_THREAD_NAME = @"<meta property=""og:title"" content=""(.*)"" \/>";
        private const string REGEX_PAGE = @"pageNav-page.*\/page-(.*)"">.*[^<]*<\/ul>";
        private const string REGEX_ATTACHMENT = @"<a href=""\/attachments\/(.*)\/"" target=""_blank""";
        private const string REGEX_IMAGE = @"class=""lbContainer-zoomer .* data-src=""(.*)"" aria-label=""Zoom"">";
        private const string REGEX_LINK_EXTERNAL = @"<a href=.* class=""link link--external"".*><img.*data-src=""(.*)"" data-url=.*\/>";
        private const string REGEX_THREAD = @"next.voz.vn\/t\/([^\/]*)";
        private const string REGEX_Get_PAGE_FROM_URL = @"\/page-(.*)";
        private const string REGEX_GET_TOKEN = @"data-csrf=""(.*)""";

        public static string GetThreadName(string html)
        {
            return Regex.Match(html, REGEX_THREAD_NAME, RegexOptions.Multiline).Groups[1].Value;
        }

        public static int GetTotalPage(string html)
        {
            var page = Regex.Match(html, REGEX_PAGE, RegexOptions.Multiline).Groups[1].Value;

            return Int32.Parse(page);
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
}
