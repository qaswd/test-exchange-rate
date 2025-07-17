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

        /// <summary>
        /// Получить курс валюты по коду и дате или список всех валют на указанную дату.
        /// </summary>
        /// <param name="code">Код валюты (например, USD). Необязательный параметр.</param>
        /// <param name="date">Дата курса (в формате yyyy-MM-dd). Если не указано — используется текущая дата.</param>
        /// <returns>Один курс валюты или список всех валют.</returns>
        /// <response code="200">Успешно. Возвращает данные о курсе(ах) валют.</response>
        /// <response code="204">Валюта с указанным кодом не найдена.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(204)]
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
