using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAgregator.Models;
using NewsAgregator.Models.Parser;
using NewsAgregator.Models.Parser.Sources;

namespace NewsAgregator.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext Context { get; }

        public HomeController(AppDbContext context)
        {
            Context = context;
        }

        MainParser parser = new MainParser();
        IParser<News> parserAW = new AWParser();
        IParser<News> parserFAVT = new FAVTParser();
        IParser<News> parserFG = new FGParser();
        IParser<News> parserRC = new RosCosmosParser();

        public IActionResult Index()
        {
            var dataBase = Context.News;
            IQueryable<News> dbNews = dataBase;
            //var dbNews = Context.News.ToList();

            // получаем список новостей.

            var news = parserAW.ParseListNews(parser.ParsePage(parserAW.BaseUrl));
            news.AddRange(parserFAVT.ParseListNews(parser.ParsePage(parserFAVT.BaseUrl)));
            news.AddRange(parserFG.ParseListNews(parser.ParsePage(parserFG.BaseUrl)));
            news.AddRange(parserRC.ParseListNews(parser.ParsePage(parserRC.BaseUrl)));

            news.Sort((x, y) => y.Date.CompareTo(x.Date));

            // добавляем в БД новые новости
            foreach (var item in news)
            {
                if (CheckNovelties(dataBase.ToList(), item))
                    dataBase.Add(item);
            }
            Context.SaveChanges();
            //dataBase.RemoveRange(dataBase);
            dbNews = dataBase.OrderByDescending(s => s.Date); // сортируем новости в БД по дате.
            //Context.SaveChanges();

            return View(dbNews.ToList());
        }

        /// <summary>
        /// Открывает новость полностью.
        /// </summary>
        /// <param name="id">Индекс новости в списке новостей.</param>
        /// <returns></returns>
        public IActionResult OpenNews(int id)
        {
            List<News> listNews = Context.News.OrderByDescending(s => s.Date).ToList();
            string url = listNews[id].NewsURL;

            News news;

            // проверка, с какого сайта новость и ее парсинг

            if (url.Contains("aviation21.ru"))
                news = parserAW.ParseNews(parser.ParsePage(url), url);

            else if (url.Contains("favt.ru"))
                news = parserFAVT.ParseNews(parser.ParsePage(url), url);

            else if (url.Contains("flightglobal.com"))
                news = parserFG.ParseNews(parser.ParsePage(url), url);

            else if (url.Contains("roscosmos.ru"))
                news = parserRC.ParseNews(parser.ParsePage(url), url);

            else return Redirect("~/Home/Index");

            return View(news);
        }

        /// <summary>
        /// Проверка новости на новизну.
        /// </summary>
        /// <param name="listOldNews">Список новостей, уже добавленных в БД.</param>
        /// <param name="newNews">Новость на проверку.</param>
        /// <returns></returns>
        bool CheckNovelties(List<News> listOldNews, News newNews)
        {
            foreach (var news in listOldNews)
            {
                if (news.Title == newNews.Title && news.Date == newNews.Date)
                    return false;
            }
            return true;
        }
    }
}