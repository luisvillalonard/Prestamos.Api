using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Seguridad;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.Prestamos;

public partial class PrestamoPago
{
    [Key]
    public long Id { get; set; }

    public long PrestamoId { get; set; }

    public int FormaPagoId { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Deuda { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal MultaMora { get; set; }

    public int UsuarioId { get; set; }

    [ForeignKey("FormaPagoId")]
    [InverseProperty("PrestamoPago")]
    public virtual FormaPago FormaPago { get; set; } = null!;

    [ForeignKey("PrestamoId")]
    [InverseProperty("PrestamoPago")]
    public virtual Prestamo Prestamo { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("PrestamoPago")]
    public virtual Usuario Usuario { get; set; } = null!;
}
