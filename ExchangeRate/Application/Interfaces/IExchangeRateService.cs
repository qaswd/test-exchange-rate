using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<List<CurrencyRateDto>> GetAllRatesAsync(DateTime date);
        Task<CurrencyRateDto> GetRateByCodeAsync(DateTime date, string code);
    }

}
