using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;

namespace NewsAgregator.Models.Parser.Sources
{
    /// <summary>
    /// Парсер новостей с сайта FlightGlobal.
    /// </summary>
    public class FGParser : IParser<News>
    {
        public string BaseUrl { get; set; } = "https://www.flightglobal.com/1013.type";

        public List<News> ParseListNews(IHtmlDocument document)
        {
            List<News> newsList = new List<News>(); // Хранит объекты новостей News.

            IEnumerable<IElement> items = document.QuerySelectorAll("div.listBlocks li"); // Получить массив блоков новостей.

            foreach (var item in items)
            {
                // Заполнение полей новости.
                News news = new News()
                {
                    Title = item.QuerySelector("h3 a")?.TextContent,
                    ImageSrc = item.QuerySelector("img")?.GetAttribute("data-src"),
                    Text = item.QuerySelector("p + p")?.TextContent,
                    NewsURL = item.QuerySelector("a")?.GetAttribute("href") ?? BaseUrl,
                    Date = Convert.ToDateTime(item.QuerySelector("span.date")?.TextContent ?? DateTime.Now.ToString())
                };
                newsList.Add(news);
            }
            return newsList;
        }

        public News ParseNews(IHtmlDocument document, string url)
        {
            IElement header = document.QuerySelector("div.headerWrapper");
            IElement container = document.QuerySelector("div.storyContentWrapper");

            string mainText = "";

            // некоторые статьи доступны полностью только по подписке, поэтому нужна проверка на null
            if (header == null && container == null)
            {
                header = document.QuerySelector("div.storyPreview");
                container = document.QuerySelector("div.standfirst");
            }

            foreach (var p in container.QuerySelectorAll("p"))
            {
                mainText += $"{p.TextContent}\n";
            }

            News news = new News()
            {
                Title = header.QuerySelector("h1")?.TextContent,
                ImageSrc = container.QuerySelector("img")?.GetAttribute("data-src"),
                Text = mainText,
                NewsURL = url,
                Date = Convert.ToDateTime(header.QuerySelector("span.date")?.TextContent ?? DateTime.Now.ToString())
            };

            return news;
        }
    }
}
