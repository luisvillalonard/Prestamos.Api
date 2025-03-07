using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.Clientes;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Clientes;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Extensiones;

namespace Prestamos.Api.Controllers.Clientes
{
    [ApiController]
    //[TokenAuthenticationFilter]
    [Route("api/[controller]")]
    public class ClientesController : BaseApiController
    {
        private readonly IClienteRepositorio _repositorio;
        private readonly IMapper _mapper;
        public ClientesController(IMapper mapper, IClienteRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestFilter request)
        {
            var result = await _repositorio.GetAllAsync(
                opt => opt.OrderBy(ord => ord.Nombres).ThenBy(ord => ord.Apellidos),
                opt => opt.Ciudad, opt => opt.DocumentoTipo, opt => opt.Sexo, opt => opt.Ocupacion);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<ClienteDto>>(result.Datos);
            return Ok(result);
        }

        [HttpGet("documento")]
        public async Task<IActionResult> Get([FromQuery] string documento)
        {
            if (documento is null)
                return Ok(new ResponseResult(false, "número de documento inválido."));
            
            var result = await _repositorio.FindAsync(
                opt => opt.Documento.Equals(documento),
                opt => opt.Ciudad, opt => opt.DocumentoTipo, opt => opt.Sexo, opt => opt.Ocupacion);
            if (!result.Ok)
                return Ok(result);

            var cliente = _mapper.Map<IEnumerable<ClienteDto>>(result.Datos).FirstOrDefault();
            if (cliente is null)
                return Ok(new ResponseResult(false, "número de documento no encontrado."));

            result.Datos = cliente;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto modelo)
        {
            var user = await Request.GetUser();
            if (user == null)
                return Ok(new ResponseResult(false, "Código de usuario inválido"));

            var item = _mapper.Map<Cliente>(modelo);
            item.UsuarioId = user.Id;
            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClienteDto modelo)
        {
            var user = await Request.GetUser();
            if (user == null)
                return Ok(new ResponseResult(false, "Código de usuario inválido"));

            var item = _mapper.Map<Cliente>(modelo);
            item.UsuarioIdActualizado = user.Id;
            item.FechaActualizado = DateOnly.FromDateTime(DateTime.Now);
            var result = await _repositorio.PutAsync(item);
            result.Datos = item;
            return Ok(result);
        }
    }
}
