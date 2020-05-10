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
        int pageSize = 8; // количество изначально отображаемых статей
        public static List<News> dbNews;
        public static List<News> searchList;
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

        public IActionResult Index(int? id)
        {
            int page = id ?? 0;
            var dataBase = Context.News;

            // создаем список новостных статей
            var news = parserAW.ParseListNews(parser.ParsePage(parserAW.BaseUrl));
            news.AddRange(parserFAVT.ParseListNews(parser.ParsePage(parserFAVT.BaseUrl)));
            news.AddRange(parserFG.ParseListNews(parser.ParsePage(parserFG.BaseUrl)));
            news.AddRange(parserRC.ParseListNews(parser.ParsePage(parserRC.BaseUrl)));

            //news.Sort((x, y) => y.Date.CompareTo(x.Date));

            // добавляем новые статьи
            foreach (var item in news)
            {
                if (CheckNovelties(dataBase.ToList(), item))
                    dataBase.Add(item);
            }
            Context.SaveChanges();

            dbNews = dataBase.OrderByDescending(s => s.Date).ToList(); // сортируем новости по дате
            searchList = dbNews;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest") //если запрос асинхронный, то вызываем частичное представление для подгрузки новостей
            {
                return PartialView("_Items", GetItemsPage(dbNews, page));
            }
            return View(GetItemsPage(dbNews, page));
        }

        /// <summary>
        /// Возвращает часть элементов страницы, в зависимости от номера страницы.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <returns></returns>
        private List<News> GetItemsPage(List<News> list, int page = 1)
        {
            var itemsToSkip = page * pageSize;

            return list.Skip(itemsToSkip).Take(pageSize).ToList();
        }

        /// <summary>
        /// Открывает новость полностью.
        /// </summary>
        /// <param name="id">Индекс новости в списке новостей.</param>
        /// <returns></returns>
        public IActionResult OpenNews(int id)
        {
            List<News> listNews = dbNews;
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
        private bool CheckNovelties(List<News> listOldNews, News newNews)
        {
            foreach (var news in listOldNews)
            {
                if (news.Title == newNews.Title && news.Date == newNews.Date)
                    return false;
            }
            return true;
        }

        public IActionResult Search(int? id, string searchString)
        {
            int page = id ?? 0;

            ViewBag.SearchString = searchString.ToLower();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchList = searchList.Where(s => s.Title.ToLower().Contains(searchString) || (s.Text?.ToLower().Contains(searchString) ?? false)).ToList();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest") //если запрос асинхронный, то вызываем частичное представление для подгрузки новостей
            {
                return PartialView("_Items", GetItemsPage(searchList, page));
            }

            return View(GetItemsPage(searchList, page));

            //return  View("Index", news.ToList());
        }
    }
}