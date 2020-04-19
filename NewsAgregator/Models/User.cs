using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    /// <summary>
    /// Информация о пользователе.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // TODO: продумать привязку и сохранение комментов.
        //public List<Comment> Comments { get; set; }
    }
}
