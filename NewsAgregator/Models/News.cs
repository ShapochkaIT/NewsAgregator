using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    public class News
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string OriginalNewsURL { get; set; }
        public DateTime Date { get; set; }
    }
}
