using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    /// <summary>
    /// Источники новостей.
    /// </summary>
    public class NewsSources
    {
        //private static List<string> sources = new List<string>()
        //{
        //    "https://aviation21.ru/category/novosti-aviacii/feed/",
        //    "https://www.aviaport.ru/news/rss/",
        //    "http://www.ato.ru/feed/taxonomy/term/12"
        //    https://novosti-kosmonavtiki.ru/news/rss/
        //    https://aviacia-all.ru/stati/feed/

        //};

        public int Id { get; set; }
        /// <summary>
        /// Названия ресурса.
        /// </summary>
        public string NameSource { get; set; }
        /// <summary>
        /// Ссылка на ресурс.
        /// </summary>
        public string SourceURL { get; set; }

    } 
}
