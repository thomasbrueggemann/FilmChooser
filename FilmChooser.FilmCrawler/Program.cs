using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using HtmlAgilityPack;

namespace FilmChooser.FilmCrawler
{
    internal static class Program
    {
        private static void Main()
        {
            var uniqueFilms = new HashSet<string>();
            const string url = "https://en.wikipedia.org/wiki/List_of_photographic_films";

            var web = new HtmlWeb();
            var doc = web.Load(url);

            var tables = doc.DocumentNode.SelectNodes("//table[@class='wikitable']");

            var currentMake = string.Empty;

            foreach (var table in tables)
            {
                var rows = table.SelectNodes(".//tr");
                var firstRowFirstColumn = rows.First().SelectNodes(".//th")?.FirstOrDefault();

                var isFilmTable = firstRowFirstColumn != null && firstRowFirstColumn.InnerText.Contains("Make");
                if (!isFilmTable) continue;

                foreach (var row in rows.Skip(1))
                {
                    var columns = row.SelectNodes(".//td");
                    if (columns.Count <= 2) continue;

                    var make = columns.ElementAt(0).InnerText.Trim();
                    var model = columns.ElementAt(1).InnerText.Trim();

                    var film = $"{make} {model}";
                    uniqueFilms.Add(film);

                    if (currentMake != make)
                    {
                        Console.WriteLine($"Scrape film for {make}");
                    }

                    currentMake = make;
                }
            }

            var json = JsonSerializer.Serialize(uniqueFilms);
            File.WriteAllText(@"films.json", json);
            Console.WriteLine("Done! Wrote all film names to films.json");
        }
    }
}