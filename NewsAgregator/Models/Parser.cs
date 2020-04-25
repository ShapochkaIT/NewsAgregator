using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAgregator.Models
{
    public class ImParser
    {

        /// <summary>
        /// Проверяет новость на новизну.
        /// </summary>
        /// <param name="listOldNews">Коллекция старых новостей.</param>
        /// <param name="newNews">Новая новость.</param>
        /// <returns></returns>
        public bool CheckNovelties(List<News> listOldNews, News newNews)
        {
            foreach (var news in listOldNews)
            {
                if (news.Title == newNews.Title && news.Date == newNews.Date)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Возвращает коллекцию новостей из указанных источников.
        /// </summary>
        /// <param name="newsSources">Коллекция источников новостей.</param>
        /// <returns></returns>
        public List<News> ParseNews(List<NewsSources> newsSources)
        {
            List<News> arrNews = new List<News>();
            foreach (var source in newsSources)
            {
                using XmlReader reader = XmlReader.Create(source.SourceURL);

                SyndicationFeed channel = SyndicationFeed.Load(reader);

                var items = channel.Items;

                foreach (var item in items)
                {
                    News news = new News()
                    {
                        Title = item.Title.Text,
                        Text = item.Summary.Text,
                        NewsURL = item.Links[0].Uri.ToString(),
                        Date = item.PublishDate
                    };
                    if (news.Date.Day == DateTime.Now.Day) // тупо чтобы сократить объем для обработки
                    {
                        arrNews.Add(news);
                    }
                }
            }
            return arrNews;
        }


        public void DoSomething()
        {

        }
    }
}
