using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace NewsAgregator.Models.Parser
{
    /// <summary>
    /// Основной класс-парсер.
    /// </summary>
    /// <typeparam name="T">Данные любого ссылочного типа.</typeparam>
    class MainParser
    {
        HtmlLoader loader;

        //public IParser<T> Parser { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parser">Класс, реализующий интерфейс IParser.</param>
/*        public MainParser(IParser<T> parser)
        {
            Parser = parser;
            loader = new HtmlLoader(Parser.BaseUrl);
        }*/

        /// <summary>
        /// Возвращает конечный результат парсинга кода страницы.
        /// </summary>
        /// <returns>Результат с данными ссылочного типа.</returns>
        public IHtmlDocument ParsePage(string url)
        {
            loader = new HtmlLoader(url);
            string source = loader.GetSourceByPage().Result; // Получаем код страницы

            //TODO: почитать подробнее про IHtmlDocument и HtmlParser.
            HtmlParser domParser = new HtmlParser();
            IHtmlDocument document = domParser.ParseDocument(source);
            //T result = Parser.ParseListNews(document);
            return document;
        }

    }
}
