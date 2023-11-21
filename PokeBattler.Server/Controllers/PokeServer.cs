using Microsoft.AspNetCore.Mvc;

namespace PokeBattler.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokeServer : ControllerBase
    {
        private readonly ILogger<PokeServer> logger;

        public PokeServer(ILogger<PokeServer> logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public void Get()
        {
            return;
        }
    }
}