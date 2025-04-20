using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prestamos.Infraestructure.TEMP;

[Keyless]
public partial class VwPrestamoCuota
{
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
    public decimal Descuento { get; set; }

    [Column(TypeName = "numeric(18, 2)")]
    public decimal SaldoFinal { get; set; }

    public bool Pagado { get; set; }

    [Column(TypeName = "numeric(38, 2)")]
    public decimal? MontoPagado { get; set; }
}
