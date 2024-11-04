using Microsoft.AspNetCore.Mvc;

namespace WebApplication10.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetSecret()
        {
            var secretValue = _configuration["TestData:SecretValue"];
            return Ok(secretValue);
        }
    }
}
