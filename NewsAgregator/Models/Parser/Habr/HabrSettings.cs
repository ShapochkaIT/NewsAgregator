using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser.Habr
{
    public class HabrSettings : IParserSettings
    {
        public HabrSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BaseUrl { get; set; } = "https://aviacia-all.ru/stati/"; //здесь прописываем url сайта.
        public string Postfix { get; set; } = "page{CurrentId}"; //вместо CurrentID будет подставляться номер страницы
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
