using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Modelos;

namespace Prestamos.Core.Interfaces.Prestamos
{
    public interface IPrestamoRepositorio : IRepositorioGenerico<Prestamo, long>
    {
        Task<ResponseResult> Todos(RequestFilter requestFilter);
        Task<ResponseResult> ReengancheAsync(Prestamo entity);
        Task<ResponseResult> Carga(Prestamo[] items);
    }
}
