using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExchangeRate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService service;

        public ExchangeRateController(IExchangeRateService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string code = null, [FromQuery] DateTime? date = null)
        {
            var targetDate = date ?? DateTime.Today;

            if (string.IsNullOrWhiteSpace(code))
            {
                var allRates = await this.service.GetAllRatesAsync(targetDate);
                return Ok(allRates);
            }

            var rate = await this.service.GetRateByCodeAsync(targetDate, code);
            
            if (rate == null)
            {
                return this.NoContent();
            }

            return this.Ok(rate);
        }
    }
}
