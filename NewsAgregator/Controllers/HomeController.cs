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

        ParserWorker<List<News>> parserAW = new ParserWorker<List<News>>(new AWParser());
        ParserWorker<List<News>> parserFAVT = new ParserWorker<List<News>>(new FAVTParser());
        ParserWorker<List<News>> parserFG = new ParserWorker<List<News>>(new FGParser());
        ParserWorker<List<News>> parserRC = new ParserWorker<List<News>>(new RosCosmosParser());

        public IActionResult Index()
        {
            //var dbNews = Context.News.ToList();

            // получаем список новостей за последний день.
            //var news = parser.ParseNews(Context.NewsSources.ToList());
            var news = parserAW.Worker();
            news.AddRange(parserFAVT.Worker());
            news.AddRange(parserFG.Worker());
            news.AddRange(parserRC.Worker());

            //// удаляем из полученного списка новости, существующие в БД.
            //for (int i = news.Count - 1; i >= 0; i--)
            //{
            //    if (!parser.CheckNovelties(dbNews, news[i]))
            //    {
            //        news.Remove(news[i]);
            //    }
            //}

            news.Sort((x, y) => y.Date.CompareTo(x.Date));

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