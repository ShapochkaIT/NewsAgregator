using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser.Sources
{
    public class FAVTParser : IParser<List<News>>
    {
        public string BaseUrl { get; set; } = "https://favt.ru/novosti-sertifikacii-avia-tehniky/";

        public List<News> ParseListNews(IHtmlDocument document)
        {
            List<News> newsList = new List<News>(); // Хранит объекты новостей News.

            //IEnumerable<IElement> items = document.QuerySelector("tbody tr[valign=top] td:nth-child(2)").QuerySelectorAll("div.news-unit"); // Получить массив блоков новостей.
            //IEnumerable<IElement> items = document.QuerySelector("tbody").QuerySelector("tr[valign=top]").QuerySelector("td:nth-child(2)").QuerySelectorAll("div.news-unit"); // Получить массив блоков новостей.
            IEnumerable<IElement> items = document.QuerySelectorAll("div.news-unit"); // Получить массив блоков новостей.

            foreach (var item in items)
            {
                var title = item.QuerySelector("div.news-unit-title").QuerySelector("a");
                //var url = item.QuerySelector("a").GetAttribute("href");
                // Заполнение полей новости.
                News news = new News()
                {
                    Title = title?.TextContent ?? item.QuerySelector("div.news-unit-title").TextContent,
                    ImageSrc = item.QuerySelector("img")?.GetAttribute("src"),
                    //Text = item.QuerySelector("p").TextContent,
                    NewsURL = BaseUrl + item.QuerySelector("a")?.GetAttribute("href"),
                    Date = Convert.ToDateTime(item.QuerySelector("div.news-unit-date").TextContent)
                };
                newsList.Add(news);
            }
            return newsList;
        }
    }
}
