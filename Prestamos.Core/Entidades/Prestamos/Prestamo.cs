using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Seguridad;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.Prestamos;

public partial class Prestamo
{
    [Key]
    public long Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    public int ClienteId { get; set; }

    public DateOnly FechaCredito { get; set; }

    public int FormaPagoId { get; set; }

    public int MetodoPagoId { get; set; }

    public int MonedaId { get; set; }

    public int Cuotas { get; set; }

    public int FechaCuotas { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal DeudaInicial { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Interes { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Capital { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Amortizacion { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal SaldoFinal { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal AbonoCuota { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal MultaRetrazo { get; set; }

    public DateOnly FechaPago { get; set; }

    public int EstadoId { get; set; }

    public int DiasMora { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Destino { get; set; } = null!;

    public int? AcesorId { get; set; }

    public int UsuarioId { get; set; }

    public DateOnly? FechaActualizado { get; set; }

    public int? UsuarioIdActualizado { get; set; }

    [ForeignKey("AcesorId")]
    [InverseProperty("Prestamo")]
    public virtual Acesor? Acesor { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("Prestamo")]
    public virtual Cliente Cliente { get; set; } = null!;

    [ForeignKey("EstadoId")]
    [InverseProperty("Prestamo")]
    public virtual PrestamoEstado Estado { get; set; } = null!;

    [ForeignKey("FormaPagoId")]
    [InverseProperty("Prestamo")]
    public virtual FormaPago FormaPago { get; set; } = null!;

    [ForeignKey("MetodoPagoId")]
    [InverseProperty("Prestamo")]
    public virtual MetodoPago MetodoPago { get; set; } = null!;

    [InverseProperty("Prestamo")]
    public virtual ICollection<PrestamoPago> PrestamoPago { get; set; } = new List<PrestamoPago>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("PrestamoUsuario")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("UsuarioIdActualizado")]
    [InverseProperty("PrestamoUsuarioIdActualizadoNavigation")]
    public virtual Usuario? UsuarioIdActualizadoNavigation { get; set; }
}
