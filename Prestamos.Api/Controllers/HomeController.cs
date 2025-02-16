using Microsoft.AspNetCore.Mvc;

namespace Prestamos.Api.Controllers
{
    [ApiController]
    [Route("/")]
    [Route("/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Init()
        {
            return "Prestamos Api - Esta en linea";
        }
    }
}
