using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace NewsAgregator.Models.Parser
{
    /// <summary>
    /// Основной класс-парсер.
    /// </summary>
    /// <typeparam name="T">Данные любого ссылочного типа.</typeparam>
    class ParserWorker<T> where T : class
    {
        HtmlLoader loader;

        public IParser<T> Parser { get; set; }

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        /// <param name="parser">Класс, реализующий интерфейс IParser.</param>
        public ParserWorker(IParser<T> parser)
        {
            Parser = parser;
            loader = new HtmlLoader(Parser.BaseUrl);
        }

        /// <summary>
        /// Возвращает конечный результат парсинга кода страницы.
        /// </summary>
        /// <returns>Результат с данными ссылочного типа.</returns>
        public T Worker()
        {
            string source = loader.GetSourceByPage().Result; // Получаем код страницы

            //TODO: почитать подробнее про IHtmlDocument и HtmlParser.
            HtmlParser domParser = new HtmlParser();
            IHtmlDocument document = domParser.ParseDocument(source);
            T result = Parser.Parse(document);
            return result;
        }

    }
}
