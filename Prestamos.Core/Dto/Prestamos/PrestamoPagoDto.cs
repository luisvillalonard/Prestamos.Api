using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Seguridad;

namespace Prestamos.Core.Dto.Prestamos;

public partial class PrestamoPagoDto
{
    public long Id { get; set; }
    public long PrestamoId { get; set; }
    public FormaPagoDto FormaPago { get; set; } = null!;
    public decimal Monto { get; set; }
    public decimal Deuda { get; set; }
    public decimal MultaMora { get; set; }
    public UsuarioDto? Usuario { get; set; }
}
