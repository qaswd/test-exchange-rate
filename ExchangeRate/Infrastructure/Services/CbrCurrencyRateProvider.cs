using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    public class CbrCurrencyRateProvider : ICurrencyRateProvider
    {
        private readonly HttpClient httpClient;

        public CbrCurrencyRateProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<CurrencyRate>> GetRatesAsync(DateTime date)
        {
            var url = $"https://www.cbr.ru/scripts/XML_daily.asp?date_req={date:dd.MM.yyyy}";

            var encoding = Encoding.GetEncoding("windows-1251");
            using var reader = new StreamReader(await this.httpClient.GetStreamAsync(url), encoding);
            string xml = await reader.ReadToEndAsync();

            var doc = XDocument.Parse(xml);
            var result = new List<CurrencyRate>();

            foreach (var valute in doc.Descendants("Valute"))
            {
                result.Add(new CurrencyRate
                {
                    Code = valute.Element("CharCode")?.Value,
                    Name = valute.Element("Name")?.Value,
                    Value = decimal.Parse(valute.Element("Value")?.Value ?? "0", new CultureInfo("ru-RU")),
                    Nominal = int.Parse(valute.Element("Nominal")?.Value ?? "1")
                });
            }

            return result;
        }
    }
}
