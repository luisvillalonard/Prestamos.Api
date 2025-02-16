using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Modelos;

namespace Prestamos.Core.Interfaces.Seguridad
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario, int>
    {
        Task<ResponseResult> ValidarAsync(LoginDto item);
        Task<ResponseResult> PostCambiarClaveAsync(UsuarioCambioClaveDto item);
    }
}
