using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
