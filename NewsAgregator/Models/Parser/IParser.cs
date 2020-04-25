using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser
{
    interface IParser<T> where T : class //класс реализующие этот интерфейс смогут возвращаться данные любого ссылочного типа
    {
        T Parse(IHtmlDocument document); // тип T при реализации будет заменяться на любой другой тип
    }
}
