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
        List<News> list = new List<News>();
        ParserWorker<News[]> parser_habr = new ParserWorker<News[]>(new HabrParser());

        //public void Parser_OnNewData(object o, string[] str) { list.AddRange(str); }
        //Настройки для парсера

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
            parser_habr.Settings = new HabrSettings(1, 2);

            //parser_habr.Start();

            list.AddRange(parser_habr.Worker());
            //parser_habr.OnNewData += Parser_OnNewData;
            return View(list);
        }
    }
}