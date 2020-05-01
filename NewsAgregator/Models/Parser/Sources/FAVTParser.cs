using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser.Sources
{
    /// <summary>
    /// Парсер новостей с сайта Федерального Агенства Воздушного Транспорта.
    /// </summary>
    public class FAVTParser : IParser<News>
    {
        public string BaseUrl { get; set; } = "https://favt.ru/novosti-sertifikacii-avia-tehniky/";

        public List<News> ParseListNews(IHtmlDocument document)
        {
            List<News> newsList = new List<News>(); // Хранит объекты новостей News.

            IEnumerable<IElement> items = document.QuerySelectorAll("div.news-unit"); // Получить массив блоков новостей.

            foreach (var item in items)
            {
                var title = item.QuerySelector("div.news-unit-title").QuerySelector("a");

                // Заполнение полей новости.
                News news = new News()
                {
                    Title = title?.TextContent ?? item.QuerySelector("div.news-unit-title").TextContent,
                    ImageSrc = item.QuerySelector("img")?.GetAttribute("src"),
                    //Text = item.QuerySelector("p").TextContent, //на сайте новости отображаются только с заголовком, без аннотации
                    NewsURL = BaseUrl + item.QuerySelector("a")?.GetAttribute("href"),
                    Date = Convert.ToDateTime(item.QuerySelector("div.news-unit-date").TextContent)
                };
                newsList.Add(news);
            }
            return newsList;
        }

        public News ParseNews(IHtmlDocument document, string url)
        {
            IElement item = document.QuerySelector("div.news-info");

            string mainText = "";

            foreach (var p in item.QuerySelectorAll("p"))
            {
                mainText += $"{p.TextContent}\n";
            }

            News news = new News()
            {
                Title = item.QuerySelector("div.news-info h2")?.TextContent,
                ImageSrc = item.QuerySelector("img")?.GetAttribute("src"),
                Text = mainText,
                NewsURL = url,
                Date = Convert.ToDateTime(item.QuerySelector("div.news-unit-date")?.TextContent ?? DateTime.Now.ToString())
            };

            return news;
        }
    }
}
