using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.Configuraciones;
using Prestamos.Core.Entidades.Configuraciones;
using Prestamos.Core.Interfaces.Configuraciones;

namespace Prestamos.Api.Controllers.Configuraciones
{
    [ApiController]
    [Route("api/configuraciones/generales")]
    public class ConfiguracionesController : ControllerBase
    {
        private readonly IConfiguracionRepositorio _repositorio;
        private readonly IMapper _mapper;
        public ConfiguracionesController(IMapper mapper, IConfiguracionRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("ultima")]
        public async Task<IActionResult> GetLast()
        {
            var result = await _repositorio.GetAllAsync();
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<ConfiguracionDto>>(result.Datos).FirstOrDefault();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConfiguracionDto modelo)
        {
            var item = _mapper.Map<Configuracion>(modelo);
            var result = await _repositorio.PostAsync(item);
            result.Datos = _mapper.Map<ConfiguracionDto>(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ConfiguracionDto modelo)
        {
            var item = _mapper.Map<Configuracion>(modelo);
            var result = await _repositorio.PutAsync(item);
            result.Datos = modelo;
            return Ok(result);
        }
    }
}
