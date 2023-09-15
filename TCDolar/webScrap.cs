﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webScrapping
{
    internal class webScrap
    {
        public static async Task<string> CallUrl(string url)
        {
            HttpClient client = new();
            var response = await client.GetStringAsync(url);
            return response;
        }

        public static string ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(html);

            //var section = htmlDoc.DocumentNode.Descendants("label").Where(node => !node.GetAttributeValue("id", "").Contains("D318")).FirstOrDefault();
            var section = htmlDoc.DocumentNode.Descendants("label").Where(node => !node.Id.Contains("D317")).FirstOrDefault(); ;

            if (section == null)
            {
                Console.WriteLine("Sección nula");
                return string.Empty;
            }
            //string tcDolar = "test";
            string tcDolar = section.InnerHtml;
            return tcDolar;
        }
    }
}
