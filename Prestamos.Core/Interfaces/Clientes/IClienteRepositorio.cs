using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Modelos;

namespace Prestamos.Core.Interfaces.Clientes
{
    public interface IClienteRepositorio : IRepositorioGenerico<Cliente, int>
    {
        Task<ResponseResult> Todos(RequestFilter request);
        Task<ResponseResult> Activos(RequestFilter request);
    }
}
