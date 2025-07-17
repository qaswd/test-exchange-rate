
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICurrencyRateProvider
    {
        Task<List<CurrencyRate>> GetRatesAsync(DateTime date);
    }
}
