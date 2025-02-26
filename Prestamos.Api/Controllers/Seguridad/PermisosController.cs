using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Enumerables;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Core.Modelos;

namespace Prestamos.Api.Controllers.Seguridad
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRolRepositorio _repositorio;
        private readonly IPermisoRepositorio _repoPermiso;
        private readonly IMenuRepositorio _repoMenu;

        public PermisosController(
            IMapper mapper,
            IRolRepositorio repositorio,
            IPermisoRepositorio repoPermiso,
            IMenuRepositorio repoMenu
        )
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _repoPermiso = repoPermiso ?? throw new ArgumentNullException(nameof(_repoPermiso));
            _repoMenu = repoMenu ?? throw new ArgumentNullException(nameof(repoMenu));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RequestFilter request)
        {
            var result = await _repositorio.GetAllAsync(
                opt => opt.OrderBy(ord => ord.Nombre),
                opt => opt.Permiso);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<RolDto>>(result.Datos);
            return Ok(result);
        }

        [HttpGet("rolId")]
        public async Task<IActionResult> GetAllAsync(int rolId)
        {
            // Obtengo los permisos asginados al rol
            var result = await _repositorio.FindAsync(rol => rol.Id == rolId, rol => rol.Permiso);
            if (!result.Ok)
                return Ok(result);

            var rol = _mapper.Map<IEnumerable<RolDto>>(result.Datos).FirstOrDefault();
            if (rol is null)
                return Ok(new ResponseResult(false, "Código de rol no encontrado."));

            // Rotorno los permisos
            return Ok(new ResponseResult(rol));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RolDto modelo)
        {
            var item = _mapper.Map<Rol>(modelo);

            // Guardo el rol
            var result = await _repositorio.PostAsync(item);
            if (!result.Ok)
                return Ok(result);

            // Establezco los nuevos permisos
            var permisos = _mapper.Map<Permiso[]>(modelo.Permisos)
                .Select(permiso => { permiso.RolId = item.Id; return permiso; })
                .AsEnumerable();

            // Guardo los permisos
            if (permisos.Any())
            {
                result = await _repoPermiso.PostRangeAsync(permisos.ToArray());
                if (!result.Ok)
                    return Ok(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RolDto modelo)
        {
            // Mapeo los datos recibidos
            var item = _mapper.Map<Rol>(modelo);

            // Actualizo los datos del rol
            var result = await _repositorio.PutAsync(item);
            if (!result.Ok)
                return Ok(result);

            // Obtengo los permisos asginados al rol
            result = await _repoPermiso.FindAsync(x => x.RolId == item.Id);
            if (!result.Ok)
                return Ok(result);

            // Si tiene permisos asignados los elimino
            var permisosAnteriores = result.Datos as IEnumerable<Permiso> ?? Enumerable.Empty<Permiso>();
            if (permisosAnteriores.Any())
            {
                result = await _repoPermiso.DeleteRangeAsync(permisosAnteriores.ToArray());
                if (!result.Ok)
                    return Ok(result);
            }

            // Establezco los nuevos permisos
            var nuevosPermisos = _mapper.Map<Permiso[]>(modelo.Permisos)
                .Select(permiso => { permiso.Id = 0; permiso.RolId = item.Id; return permiso; })
                .AsEnumerable();

            // Guardo los permisos
            if (nuevosPermisos.Any())
            {
                result = await _repoPermiso.PostRangeAsync(nuevosPermisos.ToArray());
                if (!result.Ok)
                    return Ok(result);
            }

            return Ok(result);
        }
    }
}
