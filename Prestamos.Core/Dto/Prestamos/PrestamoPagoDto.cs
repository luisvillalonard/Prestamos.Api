using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Seguridad;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Dto.Prestamos;

public partial class PrestamoPagoDto
{
    public long Id { get; set; }
    public long PrestamoId { get; set; }
    public long PrestamoCuotaId { get; set; }
    public MetodoPagoDto MetodoPago { get; set; } = new();
    public decimal Monto { get; set; }
    public decimal MultaMora { get; set; }
    public UsuarioDto? Usuario { get; set; }
    public bool Anulado { get; set; }
    public UsuarioDto? UsuarioIdAnulado { get; set; }
    public string? AnuladoFecha { get; set; }
}
