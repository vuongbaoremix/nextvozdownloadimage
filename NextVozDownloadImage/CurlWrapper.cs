using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NextVozDownloadImage
{

    public static class CurlWrapper
    {
        public static string GetString(string url, string userAgent = null, IEnumerable<string> headers = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            string arguments = buildCurlArguments(url, userAgent, headers, includeStatusCode: true);
            var (response, statusCode) = executeCurlWithStatus(arguments);

            if (statusCode != 200)
            {
                throw new InvalidOperationException($"Request failed with status code {statusCode}: {response}");
            }

            return response;
        }

        public static byte[] Download(string url, string userAgent = null, IEnumerable<string> headers = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }

            string tempFilePath = Path.GetTempFileName();

            try
            {
                string arguments = buildCurlArguments(url, userAgent, headers, includeStatusCode: true) + $" -o \"{tempFilePath}\"";
                var (_, statusCode) = executeCurlWithStatus(arguments);

                if (statusCode != 200)
                {
                    throw new InvalidOperationException($"Download failed with status code {statusCode}");
                }

                return File.ReadAllBytes(tempFilePath);
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }

        private static string buildCurlArguments(string url, string userAgent, IEnumerable<string> headers, bool includeStatusCode = false)
        {
            var arguments = new StringBuilder();

            // Add silent mode
            arguments.Append("-s ");

            // Include HTTP status code in the output
            if (includeStatusCode)
            {
                arguments.Append("-w \"%{http_code}\" ");
            }

            // Add custom user agent if specified
            if (!string.IsNullOrWhiteSpace(userAgent))
            {
                arguments.Append($"-A \"{userAgent}\" ");
            }

            // Add custom headers if specified
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    arguments.Append($"-H \"{header}\" ");
                }
            }
            // Add the URL
            arguments.Append($"-L \"{url}\"");

            return arguments.ToString();
        }

        private static (string response, int statusCode) executeCurlWithStatus(string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "curl",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8
            };

            var process = new Process { StartInfo = startInfo };

            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            process.Start();

            // Read the output
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"curl failed with exit code {process.ExitCode} and error: {error}");
            }

            // Extract HTTP status code
            var match = Regex.Match(output, @"(\d{3})$");
            if (!match.Success)
            {
                throw new InvalidOperationException("Failed to parse HTTP status code from curl response.");
            }

            int statusCode = int.Parse(match.Value);
            string responseBody = output.Substring(0, output.Length - match.Value.Length).Trim();

            return (responseBody, statusCode);
        }
    }
}
