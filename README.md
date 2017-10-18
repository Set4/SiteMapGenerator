## SiteMapGenerator ##

Есть таблица: 


```
#!sql

   CREATE TABLE [dbo].[SiteMapItems](
   [Id] [int] IDENTITY(1,1) NOT NULL,
   [ObjectId] [uniqueidentifier] NOT NULL,
   [Identifier] [varchar](20) NOT NULL,
   [LastMod] [datetime] NOT NULL) 
```


основе записей из этой таблицы генерировать следующие xml файлы: 

```
#!xml

<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
<url><loc>{domain}/{Identifier}</loc><lastmod>{LastMod}</lastmod></url>
</urlset>

```

 где каждая запись в таблице представлена как тег url, где {domain} - настраиваемая строка в программе(config), {LastMod} - дата в формате 2016-11-23 
- каждый xml файл содержит настраиваемое кол-во строк, но не болee 50 000 строк 
- xml файл сжат в gz файл 

Главный файл sitemap.xml с ссылками на все сгенерированные файлы в формате:


```
#!xml
<sitemapindex xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
<sitemap>
 <loc>{domain}/sitemaps/{filename}</loc>
 <lastmod>{LastMod}</lastmod>
</sitemap>
</sitemapindex >
```


где каждый файл представлен как тег sitemap, {domain}- та же настраиваемая строка, filename - имя файла, LastMod - самая поздняя дата из всех url-ов внутри этого файла 

# настраиваемый в программе config #
*   key="DBDataSource" 
*   key="DBPassword" 
*   key="DomainName" -именование переменной {domain} в xml-файле
*   key="PathStorageFolder" -временная папка хранения сгенерированных xml  в виде /{folder}/
*   key="MaxCountyRecords" -Max количество записей в xml-файле
*   key="NamedXmlFile" -Начальное именование файла .xml
*   key="MainNameXmlFile" -Начальное именование основного  файла
*   key="NumberOfThreads" -Количество потоков
*   key="PathSiteMapFolder" -основная папка хранения xml  в виде (**Абсолютный путь**)

# Перед запуском программы настроить config! #