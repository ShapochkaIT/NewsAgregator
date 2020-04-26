using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser.Sources
{
    public class RosCosmosParser : IParser<List<News>>
    {
        public string BaseUrl { get; set; } = "https://www.roscosmos.ru/102/";

        public List<News> Parse(IHtmlDocument document)
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
    }
}
