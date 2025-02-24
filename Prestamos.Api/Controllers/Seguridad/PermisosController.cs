using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Enumerables;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Repositorios.Seguridad;

namespace Prestamos.Api.Controllers.Seguridad
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPermisoRepositorio _repositorio;
        private readonly IMenuRepositorio _repoMenu;

        public PermisosController(IMapper mapper, IPermisoRepositorio repositorio, IMenuRepositorio repoMenu)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _repoMenu = repoMenu ?? throw new ArgumentNullException(nameof(repoMenu));
        }

        [HttpGet("rolId")]
        public async Task<IActionResult> GetAllAsync(int rolId)
        {
            // Obtengo los permisos asginados al rol
            var result = await _repositorio.FindAsync(perm => perm.RolId == rolId);
            if (!result.Ok)
                return Ok(result);

            var permisos = _mapper.Map<IEnumerable<PermisoDto>>(result.Datos).ToList();

            // Obtengo todas las opciones de menu disponibles
            result = await _repoMenu.GetAllAsync();
            if (!result.Ok)
                return Ok(result);

            var todos = _mapper.Map<IEnumerable<MenuDto>>(result.Datos);
            todos = todos.Where(x => x.Padre == (int)Menu.Ninguno)
                .Select(x => new MenuDto()
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Link = x.Link,
                    Padre = x.Padre,
                    Orden = x.Orden,
                    Activo = permisos.Any(p => p.MenuId == x.Id),
                    Items = todos.Where(p => p.Padre == x.Id)
                        .ToList()
                        .Select(a => { a.Activo = permisos.Any(p => p.MenuId == a.Id); return a; })
                        .ToList()
                })
                .OrderBy(x => x.Padre)
                    .ThenBy(x => x.Orden)
                .ToList();

            // Agrego los permisos que no tiene asignados
            result.Datos = todos;

            // Rotorno los permisos
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PermisoDto[] modelo)
        {
            // Obtengo los permisos asginados al rol
            var result = await _repositorio.FindAsync(x => x.RolId == modelo.First().RolId, x => x.Rol);
            if (!result.Ok)
                return BadRequest(result);

            // Elimino los permisos anteriores
            var permisos = _mapper.Map<IEnumerable<PermisoDto>>(result.Datos).ToList();
            if (permisos.Count() > 0)
            {
                result = await _repositorio.DeleteRangeAsync(permisos.Count());
                if (!result.Ok)
                    return BadRequest(result);
            }

            // Agrego los nuevos permisos
            permisos = _mapper.Map<IEnumerable<Permiso>>(modelo);
            result = await _repositorio.CreateRangeAsync(permisos.ToArray());
            if (!result.Ok)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
