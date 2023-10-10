using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        readonly IConfiguration _configuration;

        public FilesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("[action]")] //Buradan devam. BaseStroageUrl'den Foto getirtmiyoruz
        public IActionResult GetBaseUrl()
        {
            return Ok(_configuration["BaseStorageUrl"]);
        }

        [HttpGet("[action]")]
        public IActionResult GetLocalUrl()
        {
            var localStorageUrl = _configuration["LocalStorageUrl"];

            if (string.IsNullOrEmpty(localStorageUrl))
            {
                return BadRequest("LocalStorageUrl is not configured properly.");
            }

            return Ok(new { LocalStorageUrl = localStorageUrl });

        }
    }
}
