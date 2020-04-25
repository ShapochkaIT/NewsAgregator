using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;

namespace NewsAgregator.Models.Parser
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings; //настройки для загрузчика кода страниц
        HtmlLoader loader; //загрузчик кода страницы
        bool isActive; //активность парсера

        public IParser<T> Parser
        {
            get { return parser; }
            set { parser = value; }
        }

        public IParserSettings Settings
        {
            get { return parserSettings; }
            set
            {
                parserSettings = value; //Новые настройки парсера
                loader = new HtmlLoader(value); //сюда помещаются настройки для загрузчика кода страницы
            }
        }

        public bool IsActive //проверяем активность парсера.
        {
            get { return isActive; }
        }

        //Это событие возвращает спаршенные за итерацию данные( первый аргумент ссылка на парсер, и сами данные вторым аргументом)
        public event Action<object, T> OnNewData;
        //Это событие отвечает информирование при завершении работы парсера.
        public event Action<object> OnComplited;

        //1-й конструктор, в качестве аргумента будет передеваться класс реализующий интерфейс IParser
        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public void Start() //Запускаем парсер
        {
            isActive = true;
            Worker();
        }

        public void Stop() //Останавливаем парсер
        {
            isActive = false;
        }

        public T Worker()
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {

                string source = loader.GetSourceByPage(i).Result; //Получаем код страницы
                                                                  //Здесь магия AngleShap, подробнее об интерфейсе IHtmlDocument и классе HtmlParser, 
                                                                  //можно прочитать на GitHub, это интересное чтиво с примерами.
                HtmlParser domParser = new HtmlParser();
                IHtmlDocument document = domParser.ParseDocument(source);
                T result = parser.Parse(document);
                return result;
                //OnNewData?.Invoke(this, result);


            }
            return null;
            //OnComplited?.Invoke(this);
            //isActive = false;
        }
    }
}
