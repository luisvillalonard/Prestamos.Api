using Prestamos.Core.Modelos;

namespace Prestamos.Core.Interfaces.Seguridad
{
    public interface IMenuRepositorio
    {
        Task<ResponseResult> GetAllAsync();
        Task<ResponseResult> GetAllAsync(int rolId);
    }
}
