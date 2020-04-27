using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;

namespace NewsAgregator.Models.Parser.Sources
{
    public class FGParser : IParser<List<News>>
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
                    Title = item.QuerySelector("h3 a")?.TextContent ?? "Отсутствует текст заголовка!",
                    ImageSrc = item.QuerySelector("img")?.GetAttribute("data-src"),
                    Text = item.QuerySelector("p + p")?.TextContent ?? "Основной текст",
                    NewsURL = item.QuerySelector("a")?.GetAttribute("href"),
                    Date = Convert.ToDateTime(item.QuerySelector("span.date")?.TextContent ?? "01.01.1111")
                };
                newsList.Add(news);
            }
            return newsList;
        }
    }
}
