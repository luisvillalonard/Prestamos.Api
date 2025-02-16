﻿using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Prestamos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.DataMaestra;

public partial class MetodoPago
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("MetodoPago")]
    public virtual ICollection<Prestamo> Prestamo { get; set; } = new List<Prestamo>();
}
