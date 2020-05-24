using Gofbd.Core;
using Gofbd.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gofbd.Web.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching", "Corona"
        };

        private readonly ILogger _logger;
        private readonly IDataContextFactory dataContextFactory;
        private readonly IDataContext dataContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IDataContextFactory dataContextFactory,
            IDataContext dataContext)
        {
            _logger = logger;
            this.dataContextFactory = dataContextFactory;
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            //using (var dataContext = await this.dataContextFactory.Create())
            //{
            //    var product = dataContext.Get<Product>().First();
            //    this._logger.LogInformation($"Product name {product.Name} from data context factory.");
            //}
            //var pro = this.dataContext.Get<Product>().First();
            //this._logger.LogInformation($"Product name {pro.Name} from data context.");
            var name = "Serilog";
            this._logger.LogInformation("test message from {name}", name);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
