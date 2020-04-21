using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    /// <summary>
    /// Представляет конкретную новость.
    /// </summary>
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        //TODO: добавить теги.
        public string NewsURL { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
