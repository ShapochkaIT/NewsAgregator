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

            //получаем список новостей.

            var news = parserAW.ParseListNews(parser.ParsePage(parserAW.BaseUrl));
            news.AddRange(parserFAVT.ParseListNews(parser.ParsePage(parserFAVT.BaseUrl)));
            news.AddRange(parserFG.ParseListNews(parser.ParsePage(parserFG.BaseUrl)));
            news.AddRange(parserRC.ParseListNews(parser.ParsePage(parserRC.BaseUrl)));

            news.Sort((x, y) => y.Date.CompareTo(x.Date));

            //удаляем из полученного списка новости, существующие в БД.
            foreach (var item in news)
            {
                if (CheckNovelties(dataBase.ToList(), item))
                    dataBase.Add(item);
            }

/*            for (int i = news.Count - 1; i >= 0; i--)
            {
                else news.Remove(news[i]);
            }*/
            Context.SaveChanges();
            //dataBase.RemoveRange(dataBase);
            dbNews = dataBase.OrderByDescending(s => s.Date);
            //Context.SaveChanges();

            //return View(dataBase.ToList());
            return View(dbNews.ToList());
        }

        public IActionResult OpenNews(int id)
        {
            List<News> listNews = Context.News.OrderByDescending(s => s.Date).ToList();
            string url = listNews[id].NewsURL;

            News news;

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