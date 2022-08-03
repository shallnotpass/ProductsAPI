using HtmlAgilityPack;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HTMLParser
{
    public class Parser : IParser
    {
        private readonly string _url = @"https://www.tks.ru/db/tnved/search?searchstr=";
        private static async Task<string> CallUrl(string fullUrl)
        {
            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }
        private async Task<string> GetAjaxData(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
            client.DefaultRequestHeaders.Referrer = new Uri(url);

            var resp = await client.GetStringAsync(@"https://www.tks.ru/db/tnved/tree/c6102301000/show/ajax");

            return resp;
        }

        public async Task<IEnumerable<string>> GetDataByNomenclature(string nomenclature, CancellationToken token)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(await CallUrl(_url + nomenclature));
            var searchedResults = htmlDoc.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("class", "").Contains("tnved")).FirstOrDefault();
            var nomenclatures = searchedResults
                .Descendants("li")
                .Select(node => node.Descendants("a").FirstOrDefault())
                .ToList();

            List<string> longNomencaltures = new List<string>();

            foreach (var link in nomenclatures)
            {
                if (link != null)
                {
                    var text = link.InnerText.Skip(8);
                    if(link.InnerText.Length == 15) 
                        longNomencaltures.Add(String.Concat(text));
                }
                    
            }

            return longNomencaltures;
        }
    }
}