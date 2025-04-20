using Prestamos.Core.Dto.Prestamos;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Modelos;

namespace Prestamos.Core.Interfaces.Prestamos
{
    public interface IPrestamoPagoRepositorio : IRepositorioGenerico<PrestamoPago, long>
    {
        Task<ResponseResult> PostAsync(PrestamoPago item, long prestamoId);
        Task<ResponseResult> PostAutomaticoAsync(PrestamoPagoDto[] items);
    }
}
