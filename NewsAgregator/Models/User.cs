using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    public class User
    {
        public string Name { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
