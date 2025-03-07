using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Prestamos;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Extensiones;

namespace Prestamos.Api.Controllers.Prestamos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoRepositorio _repositorio;
        private readonly IMapper _mapper;
        public PrestamosController(IMapper mapper, IPrestamoRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestFilter request)
        {
            var result = await _repositorio.GetAllAsync(
                opt => opt.OrderByDescending(ord => ord.Id),
                opt => opt.Cliente, opt => opt.FormaPago, opt => opt.MetodoPago, 
                opt => opt.Estado, opt => opt.Acesor!, opt => opt.PrestamoCuota);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<PrestamoDto>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PrestamoDto modelo)
        {
            var user = await Request.GetUser();
            if (user == null)
                return Ok(new ResponseResult(false, "Código de usuario inválido"));

            var item = _mapper.Map<Prestamo>(modelo);
            item.UsuarioId = user.Id;
            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PrestamoDto modelo)
        {
            var user = await Request.GetUser();
            if (user == null)
                return Ok(new ResponseResult(false, "Código de usuario inválido"));

            var item = _mapper.Map<Prestamo>(modelo);
            item.UsuarioIdActualizado = user.Id;
            item.FechaActualizado = DateOnly.FromDateTime(DateTime.Now);
            var result = await _repositorio.PutAsync(item);
            return Ok(result);
        }
    }
}
