using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Enumerables;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Helpers;

namespace Prestamos.Infraestructure.Repositorios.Seguridad
{
    public class MenuRepositorio
    {
        public async Task<ResponseResult> GetAllAsync()
        {
            return await Task.FromResult(new ResponseResult()
            {
                Datos = Todos()
            });
        }

        private MenuDto[] Todos()
        {
            return Enum.GetValues(typeof(Menu))
                .Cast<Menu>()
                .Select(item => item.GetMenuAttribute())
                .ToList()
                .Where(item => item != null)
                .Select(item => new MenuDto()
                {
                    Id = item!.Id,
                    Titulo = item.Titulo,
                    Link = item.Link,
                    Padre = item.Padre,
                    Orden = item.Orden
                }).ToArray();
        }
    }
}
