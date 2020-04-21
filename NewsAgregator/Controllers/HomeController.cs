using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using NewsAgregator.Models;

namespace NewsAgregator.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext Context { get; }

        public HomeController(AppDbContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            AddNewsInDB();

            return View(Context.News.ToList());
        }

        private bool CheckNovelties(SyndicationItem item)
        {
            var list = Context.News.ToList();
            foreach (var news in list)
            {
                if (news.Title == item.Title.Text && news.Date == item.PublishDate)
                    return false;
            }
            return true;
        }

        private void AddNewsInDB()
        {
            var newsSources = Context.NewsSources.ToList();
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
                        //TODO: настроить нормальное отображение даты-времени
                        //Date = (DateTimeOffset) item.PublishDate
                    };

                    if (CheckNovelties(item))
                    {
                        Context.News.Add(news);
                        Context.SaveChanges();
                    }
                }
            }
        }
    }
}