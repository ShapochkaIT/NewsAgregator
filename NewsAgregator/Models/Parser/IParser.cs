using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace NewsAgregator.Models.Parser
{
    /// <summary>
    /// Интерфейс для сайта, являющегося целью парсинга.
    /// </summary>
    /// <typeparam name="T">Данные любого ссылочного типа.</typeparam>
    interface IParser<T> where T : class //класс реализующие этот интерфейс смогут возвращаться данные любого ссылочного типа
    {
        /// <summary>
        /// Содержит URL сайта.
        /// </summary>
        string BaseUrl { get; set; } //url сайта
        /// <summary>
        /// Парсит документ с кодом страницы со списком новостей.
        /// </summary>
        /// <param name="document">Документ, содержащий код страницы.</param>
        /// <returns></returns>
        List<T> ParseListNews(IHtmlDocument document); // тип T при реализации будет заменяться на любой другой тип

        T ParseNews(IHtmlDocument document, string url);
    }
}
