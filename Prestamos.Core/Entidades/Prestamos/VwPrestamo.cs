using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prestamos.Infraestructure.TEMP;

[Keyless]
public partial class VwPrestamo
{
    public long Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    public DateOnly FechaCredito { get; set; }

    [StringLength(301)]
    [Unicode(false)]
    public string Cliente { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ClienteCodigo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ClienteDocumento { get; set; } = null!;

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal Interes { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal? Pendiente { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Estado { get; set; } = null!;

    public bool Activo { get; set; }

    public bool Cancelado { get; set; }
}
