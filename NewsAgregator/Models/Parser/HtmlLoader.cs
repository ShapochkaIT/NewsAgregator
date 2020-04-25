using System;
using System.Collections.Generic;
using System.Linq;
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
        readonly HttpClient client; //для отправки HTTP запросов и получения HTTP ответов.
        readonly string url; //сюда будем передовать адрес.

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# App"); //Это для индентификации на сайте-жертве.
            url = $"{settings.BaseUrl}/{settings.Postfix}/"; //Здесь собирается адресная строка
        }

        public async Task<string> GetSourceByPage(int id) // id - это id страницы
        {
            string currentUrl = url.Replace("{CurrentId}", id.ToString());//Подменяем {CurrentId} на номер страницы
            HttpResponseMessage responce = await client.GetAsync(currentUrl); //Получаем ответ с сайта.
            string source = default;

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                source = await responce.Content.ReadAsStringAsync(); //Помещаем код страницы в переменную.
            }
            return source;
        }
    }
}
