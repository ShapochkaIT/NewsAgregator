# NewsAgregator
Данный проект является агрегатором новостей по тематике "Авиационная промышленность".

Производится парсинг HTML-разметки 4-х сайтов-источников:
* https://www.roscosmos.ru/102/
* https://www.flightglobal.com/1013.type
* https://favt.ru/novosti-sertifikacii-avia-tehniky/
* https://aviation21.ru/category/novosti-aviacii/

Все новостные статьи добавляются в БД, после чего, отформатированные по дате выводятся в виде списка в браузер.

Приложение реализовано на ASP.NET Core 3.1, с использованием Entity Framework Core и AngleSharp.

Реализован функционал:
1. Получение списка последних новостей с вышеуказанных ресурсов;
2. Поиск новостей по заданным словам;
3. Сортировка новостей по дате(от новых к старым);
4. Открытие полной новости на сайте;
