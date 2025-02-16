using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.Prestamos;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;

namespace Prestamos.Api.Controllers.Prestamos
{
    [Route("api/prestamos/pagos")]
    [ApiController]
    public class PrestamosPagosController : ControllerBase
    {
        private readonly IPrestamoPagoRepositorio _repositorio;
        private readonly IMapper _mapper;
        public PrestamosPagosController(IMapper mapper, IPrestamoPagoRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestFilter request)
        {
            var result = await _repositorio.GetAllAsync(
                opt => opt.OrderByDescending(ord => ord.Id));
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<PrestamoPagoDto>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PrestamoPagoDto modelo)
        {
            var item = _mapper.Map<PrestamoPago>(modelo);
            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PrestamoPagoDto modelo)
        {
            var item = _mapper.Map<PrestamoPago>(modelo);
            var result = await _repositorio.PutAsync(item);
            return Ok(result);
        }
    }
}
