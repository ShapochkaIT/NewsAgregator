using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public News News { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
    }
}
