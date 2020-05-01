using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAgregator.Models.Parser.Sources
{
    /// <summary>
    /// Парсер новостей с сайта "Авиация России".
    /// </summary>
    public class AWParser : IParser<News>
    {
        public string BaseUrl { get; set; } = "https://aviation21.ru/category/novosti-aviacii/";

        public List<News> ParseListNews(IHtmlDocument document)
        {
            List<News> newsList = new List<News>(); // Хранит объекты новостей News.

            IEnumerable<IElement> items = document.QuerySelectorAll("article"); // Получить массив блоков новостей.

            foreach (var item in items)
            {
                // Заполнение полей новости.
                News news = new News()
                {
                    Title = item.QuerySelector("h2")?.TextContent,
                    ImageSrc = item.QuerySelector("img")?.GetAttribute("src"),
                    Text = item.QuerySelector("p")?.TextContent,
                    NewsURL = item.QuerySelector("a")?.GetAttribute("href") ?? BaseUrl,
                    Date = Convert.ToDateTime(item.QuerySelector("time")?.GetAttribute("datetime") ?? DateTime.Now.ToString())
                };
                newsList.Add(news);
            }
            return newsList;
        }

        public News ParseNews(IHtmlDocument document, string url)
        {
            IElement item = document.QuerySelector("main.site-main");

            string mainText = "";

            foreach (var p in item.QuerySelectorAll("p")) // собирает текстовые блоки в один объект
            {
                mainText += $"{p.TextContent}\n";
            }

            News news = new News()
            {
                Title = item.QuerySelector("h3.page-title")?.TextContent,
                ImageSrc = item.QuerySelector("img")?.GetAttribute("src"),
                Text = mainText,
                NewsURL = url,
                Date = Convert.ToDateTime(item.QuerySelector("time")?.GetAttribute("datetime") ?? DateTime.Now.ToString())
            };

            return news;
        }
    }
}
