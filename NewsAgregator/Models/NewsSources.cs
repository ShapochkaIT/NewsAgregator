using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    /// <summary>
    /// Список новостных источников.
    /// </summary>
    public class NewsSources
    {
        private static List<string> sources = new List<string>()
        {
            "https://aviation21.ru/category/novosti-aviacii/feed/",
            "https://rossaprimavera.ru/rss",
            "http://www.ato.ru/feed/taxonomy/term/12",

        };
        public List<string> Sources { get; }

        public void AddSource(string source)
        {
            sources.Add(source);
        }

        
    } 
}
