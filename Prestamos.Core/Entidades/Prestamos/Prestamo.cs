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

    public DateOnly FechaRegistro { get; set; }

    public DateOnly FechaCredito { get; set; }

    public int FormaPagoId { get; set; }

    public int MetodoPagoId { get; set; }

    public int MonedaId { get; set; }

    public int Cuotas { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Interes { get; set; }

    public int EstadoId { get; set; }

    public bool AplicaDescuento { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Destino { get; set; } = null!;

    public int? AcesorId { get; set; }

    public int UsuarioId { get; set; }

    public DateOnly? FechaActualizado { get; set; }

    public int? UsuarioIdActualizado { get; set; }

    public bool Cancelado { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FechaCancelado { get; set; }

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

    [ForeignKey("MonedaId")]
    [InverseProperty("Prestamo")]
    public virtual Moneda Moneda { get; set; } = null!;

    [InverseProperty("Prestamo")]
    public virtual ICollection<PrestamoCuota> PrestamoCuota { get; set; } = new List<PrestamoCuota>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("PrestamoUsuario")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("UsuarioIdActualizado")]
    [InverseProperty("PrestamoUsuarioIdActualizadoNavigation")]
    public virtual Usuario? UsuarioIdActualizadoNavigation { get; set; }
}
