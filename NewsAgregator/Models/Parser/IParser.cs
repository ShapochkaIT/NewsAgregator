using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace NewsAgregator.Models.Parser
{
    /// <summary>
    /// Интерфейс для сайта, являющегося целью парсинга.
    /// </summary>
    /// <typeparam name="T">Данные любого ссылочного типа.</typeparam>
    interface IParser<T> where T : class
    {
        /// <summary>
        /// Содержит URL сайта.
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Парсит документ с кодом страницы со списком новостей.
        /// </summary>
        /// <param name="document">Документ, содержащий код страницы.</param>
        /// <returns></returns>
        List<T> ParseListNews(IHtmlDocument document);

        /// <summary>
        /// Парсит документ с кодом страницы, содержащей новость.
        /// </summary>
        /// <param name="document">Документ, содержащий код страницы.</param>
        /// <param name="url">Ссылка на оригинальную страницу.</param>
        /// <returns></returns>
        T ParseNews(IHtmlDocument document, string url);
    }
}
