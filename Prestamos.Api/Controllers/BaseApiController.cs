using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Infraestructure.Filtros;

namespace Prestamos.Api.Controllers
{
    [ApiController]
    [TokenAuthenticationFilter]
    public class BaseApiController : ControllerBase
    {
        public UserApp? UserCurrent;

        public BaseApiController() { }
    }
}
