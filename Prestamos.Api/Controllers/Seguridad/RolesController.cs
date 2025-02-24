using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Core.Modelos;

namespace Prestamos.Api.Controllers.Seguridad
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolRepositorio _repositorio;
        private readonly IMapper _mapper;
        public RolesController(IMapper mapper, IRolRepositorio repositorio)
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

            result.Datos = _mapper.Map<IEnumerable<RolDto>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RolDto modelo)
        {
            var item = _mapper.Map<Rol>(modelo);
            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RolDto modelo)
        {
            var item = _mapper.Map<Rol>(modelo);
            var result = await _repositorio.PutAsync(item);
            return Ok(result);
        }
    }
}
