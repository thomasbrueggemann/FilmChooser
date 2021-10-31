using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using HtmlAgilityPack;

namespace FilmChooser.CameraCrawler
{
    internal static class Program
    {
        private static void Main()
        {
            var uniqueCameras = new HashSet<string>();

            for (var c = 'A'; c <= 'Z'; c++)
            {
                var url = $"https://camerapedia.fandom.com/wiki/Category:{c}";

                do
                {
                    var (cameras, nextUrl) = LoadCameras(url);

                    foreach (var camera in cameras)
                    {
                        uniqueCameras.Add(camera);
                    }

                    url = nextUrl;
                }
                while (!string.IsNullOrEmpty(url));
            }

            var json = JsonSerializer.Serialize(uniqueCameras);
            File.WriteAllText(@"cameras.json", json);
            Console.WriteLine("Done! Wrote all camera names to cameras.json");
        }

        private record CamerasResult(IEnumerable<string> Cameras, string NextUrl);

        private static CamerasResult LoadCameras(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            Console.WriteLine($"Parse {url}");

            var cameraListItems = doc.DocumentNode.SelectNodes("//li[@class='category-page__member']");

            var cameras = cameraListItems
                .Select(i => i.InnerText
                    .Replace("\t", "")
                    .Replace("\n", "")
                    .Trim());

            var nextUrl = string.Empty;
            var nextButton = doc.DocumentNode.SelectSingleNode("//a[@class='category-page__pagination-next wds-button wds-is-secondary']");
            if (nextButton != null)
            {
                nextUrl = nextButton.Attributes["href"].Value;
            }

            return new CamerasResult(cameras, nextUrl);
        }
    }
}