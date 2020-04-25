using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsAgregator.Models.Parser
{
    /// <summary>
    /// Загружает код HTML.
    /// </summary>
    class HtmlLoader
    {
        /// <summary>
        /// Клиента отправки HTTP запросов и получения HTTP ответов.
        /// </summary>
        readonly HttpClient client;

        /// <summary>
        /// Адрес страницы.
        /// </summary>
        readonly string url;

        public HtmlLoader(string url)
        {
            client = new HttpClient();
            //client.DefaultRequestHeaders.Add("User-Agent", "C# App"); //Индентификации на сайте-источнике.
            this.url = url;
        }

        /// <summary>
        /// Возвращает код страницы.
        /// </summary>
        /// <returns>Код страницы.</returns>
        public async Task<string> GetSourceByPage()
        {
            HttpResponseMessage responce = await client.GetAsync(url); //Получаем ответ с сайта.
            string source = default;

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную.
            }
            return source;
        }
    }
}
