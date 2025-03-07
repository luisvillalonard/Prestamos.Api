using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.Prestamos;

public partial class PrestamoCuota
{
    [Key]
    public long Id { get; set; }

    public long PrestamoId { get; set; }

    public DateOnly FechaPago { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal DeudaInicial { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Capital { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Interes { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Amortizacion { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal SaldoFinal { get; set; }

    [ForeignKey("PrestamoId")]
    [InverseProperty("PrestamoCuota")]
    public virtual Prestamo Prestamo { get; set; } = null!;
}
