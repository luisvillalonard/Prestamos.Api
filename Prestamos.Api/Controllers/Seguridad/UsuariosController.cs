using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Core.Modelos;

namespace Prestamos.Api.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IMapper _mapper;
        public UsuariosController(IMapper mapper, IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestFilter request)
        {
            var result = await _repositorio.GetAllAsync(
                opt => opt.OrderBy(ord => ord.Acceso));
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<UsuarioDto>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDto modelo)
        {
            var item = _mapper.Map<Usuario>(modelo);
            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UsuarioDto modelo)
        {
            var item = _mapper.Map<Usuario>(modelo);
            var result = await _repositorio.PutAsync(item);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("validar")]
        public async Task<IActionResult> ValidarUsuario([FromBody] LoginDto modelo)
        {
            var result = await _repositorio.ValidarAsync(modelo);
            return Ok(result);
        }

        [HttpPost("cambio")]
        public async Task<IActionResult> PostCambiarClave([FromBody] UsuarioCambioClaveDto modelo)
        {
            var result = await _repositorio.PostCambiarClaveAsync(modelo);
            return Ok(result);
        }
    }
}
