using System;
using System.Collections.Generic;
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

            foreach (var table in tables)
            {
                var rows = table.SelectNodes("/tr");
            }
        }
    }
}