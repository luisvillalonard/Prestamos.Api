using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Seguridad;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.Prestamos;

public partial class PrestamoPago
{
    [Key]
    public long Id { get; set; }

    public long PrestamoCuotaId { get; set; }

    public int MetodoPagoId { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal MultaMora { get; set; }

    public int UsuarioId { get; set; }

    public bool Anulado { get; set; }

    public int? UsuarioIdAnulado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AnuladoFecha { get; set; }

    [ForeignKey("MetodoPagoId")]
    [InverseProperty("PrestamoPago")]
    public virtual MetodoPago MetodoPago { get; set; } = null!;

    [ForeignKey("PrestamoCuotaId")]
    [InverseProperty("PrestamoPago")]
    public virtual PrestamoCuota PrestamoCuota { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("PrestamoPagoUsuario")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("UsuarioIdAnulado")]
    [InverseProperty("PrestamoPagoUsuarioIdAnuladoNavigation")]
    public virtual Usuario? UsuarioIdAnuladoNavigation { get; set; }
}
