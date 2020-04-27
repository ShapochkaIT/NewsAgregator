using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser.Sources
{
    public class RosCosmosParser : IParser<News>
    {
        public string BaseUrl { get; set; } = "https://www.roscosmos.ru/102/";

        public List<News> ParseListNews(IHtmlDocument document)
        {
            List<News> newsList = new List<News>(); // Хранит объекты новостей News.

            IEnumerable<IElement> items = document.QuerySelectorAll("div.newslist"); // Получить массив блоков новостей.

            foreach (var item in items)
            {
                // Заполнение полей новости.
                News news = new News()
                {
                    Title = item.QuerySelector("span.name")?.TextContent ?? "Отсутствует текст заголовка!",
                    ImageSrc = "https://www.roscosmos.ru" + item.QuerySelector("img")?.GetAttribute("src"),
                    Text = item.QuerySelector("span.anons")?.TextContent ?? "Основной текст",
                    NewsURL = "https://www.roscosmos.ru" + item.QuerySelector("a")?.GetAttribute("href"),
                    Date = Convert.ToDateTime(item.QuerySelector("span.date")?.TextContent ?? "01.01.1111")
                };
                newsList.Add(news);
            }
            return newsList;
        }

        public News ParseNews(IHtmlDocument document, string url)
        {
            IElement item = document.QuerySelector("div.newsitem");

            string mainText = "";

            foreach (var p in item.QuerySelectorAll("p"))
            {
                mainText += p.TextContent + "\n";
            }
            foreach (var p in item.QuerySelectorAll("div.block_v1"))
            {
                mainText += p.TextContent + "\n";
            }

            News news = new News()
            {
                Title = item.QuerySelector("h2")?.TextContent,
                ImageSrc = item.QuerySelector("img")?.GetAttribute("data-src"),
                Text = mainText,
                NewsURL = url,
                Date = Convert.ToDateTime(item.QuerySelector("div.date")?.TextContent ?? "01.01.1111")
            };

            return news;
        }
    }
}
