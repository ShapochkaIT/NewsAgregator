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
using NewsAgregator.Models.Parser.Habr;

namespace NewsAgregator.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext Context { get; }

        public HomeController(AppDbContext context)
        {
            Context = context;
        }

        ParserWorker<List<News>> parser_habr = new ParserWorker<List<News>>(new AWParser());

        public IActionResult Index()
        {
            //ParserWorker parser = new ParserWorker();

            //var dbNews = Context.News.ToList();

            //// получаем список новостей за последний день.
            //var news = parser.ParseNews(Context.NewsSources.ToList());

            //// удаляем из полученного списка новости, существующие в БД.
            //for (int i = news.Count - 1; i >= 0; i--)
            //{
            //    if (!parser.CheckNovelties(dbNews, news[i]))
            //    {
            //        news.Remove(news[i]);
            //    }
            //}

            //news.Sort((x, y) => y.Date.CompareTo(x.Date));

            return View(parser_habr.Worker());
        }
    }
}