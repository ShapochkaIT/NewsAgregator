using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser.Habr
{
    public class HabrParser : IParser<News[]>
    {
        public News[] Parse(IHtmlDocument document)
        {
            //Для хранения заголовков
            List<string> list = new List<string>();
            List<News> newsList = new List<News>();
            //Здесь мы получаем заголовки
            IEnumerable<IElement> items = document.QuerySelectorAll("article");
            //.Where(item => item.ClassName != null && item.ClassName.Contains("entry-title"));



            foreach (var item in items)
            {
                //Добавляем заголовки в коллекцию.
                News news = new News()
                {
                    Title = item.QuerySelector("h2").TextContent,
                    ImageSrc = item.QuerySelector("img").GetAttribute("src"),
                    Text = item.QuerySelector("p").TextContent,
                    NewsURL = item.QuerySelector("a").GetAttribute("href"),
                    Date = Convert.ToDateTime(item.QuerySelector("time").GetAttribute("datetime"))

                };
                newsList.Add(news);
            }
            return newsList.ToArray();
        }
    }
}
