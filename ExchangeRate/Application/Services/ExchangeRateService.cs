using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly ICurrencyRateProvider provider;

        public ExchangeRateService(ICurrencyRateProvider provider)
        {
            this.provider = provider;
        }

        public async Task<List<CurrencyRateDto>> GetAllRatesAsync(DateTime date)
        {
            var rates = await this.provider.GetRatesAsync(date);
            return rates.Select(r => new CurrencyRateDto
            {
                Code = r.Code,
                Name = r.Name,
                Value = r.Value,
                Nominal = r.Nominal
            }).ToList();
        }

        public async Task<CurrencyRateDto> GetRateByCodeAsync(DateTime date, string code)
        {
            var rates = await this.provider.GetRatesAsync(date);
            var match = rates.FirstOrDefault(r => r.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (match == null) return null;

            return new CurrencyRateDto
            {
                Code = match.Code,
                Name = match.Name,
                Value = match.Value,
                Nominal = match.Nominal
            };
        }
    }
}
