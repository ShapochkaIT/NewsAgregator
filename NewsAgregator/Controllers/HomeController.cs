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

        /*        MainParser<List<News>> parserAW = new MainParser<List<News>>(new AWParser());
                MainParser<List<News>> parserFAVT = new MainParser<List<News>>(new FAVTParser());
                MainParser<List<News>> parserFG = new MainParser<List<News>>(new FGParser());
                MainParser<List<News>> parserRC = new MainParser<List<News>>(new RosCosmosParser());*/

        MainParser parser = new MainParser();
        IParser<News> parserAW = new AWParser();
        IParser<News> parserFAVT = new FAVTParser();
        IParser<News> parserFG = new FGParser();
        IParser<News> parserRC = new RosCosmosParser();

        public IActionResult Index()
        {
            var dataBase = Context.News;
            var dbNews = Context.News.ToList();

            // получаем список новостей за последний день.
            //var news = parser.ParseNews(Context.NewsSources.ToList());
            /*            var news = parserAW.ParsePage();
                        news.AddRange(parserFAVT.ParsePage());
                        news.AddRange(parserFG.ParsePage());
                        news.AddRange(parserRC.ParsePage());*/
            /*
                        news.Sort((x, y) => y.Date.CompareTo(x.Date));

                        // удаляем из полученного списка новости, существующие в БД.
                        foreach (var item in news)
                        {
                            if (CheckNovelties(dbNews, item))
                                dataBase.Add(item);
                        }*/

            //for (int i = news.Count - 1; i >= 0; i--)
            //{
            //    else news.Remove(news[i]);
            //}
            //Context.SaveChanges();
            //dataBase.RemoveRange(dataBase);
            //Context.SaveChanges();

            return View(dataBase.ToList());
        }

        public IActionResult OpenNews(int id)
        {
            List<News> listNews = Context.News.ToList();
            string url = listNews[id].NewsURL;

            News news;

            if (url.Contains("roscosmos"))
            {
                news = parserRC.ParseNews(parser.ParsePage(url), url);
            }
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