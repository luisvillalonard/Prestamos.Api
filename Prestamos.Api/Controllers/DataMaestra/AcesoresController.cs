using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Modelos;

namespace Prestamos.Api.Controllers.DataMaestra
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcesoresController : ControllerBase
    {
        private readonly IAcesorRepositorio _repositorio;
        private readonly IMapper _mapper;
        public AcesoresController(IMapper mapper, IAcesorRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestFilter request)
        {
            var result = await _repositorio.GetAllAsync(
                opt => opt.OrderBy(ord => ord.Nombre));
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<AcesorDto>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AcesorDto modelo)
        {
            var item = _mapper.Map<Acesor>(modelo);
            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AcesorDto modelo)
        {
            var item = _mapper.Map<Acesor>(modelo);
            var result = await _repositorio.PutAsync(item);
            return Ok(result);
        }
    }
}
