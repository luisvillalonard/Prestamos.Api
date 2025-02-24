using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prestamos.Core.Entidades.Seguridad;

public partial class Rol
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    [InverseProperty("Rol")]
    public virtual ICollection<Permiso> Permiso { get; set; } = new List<Permiso>();

    [InverseProperty("Rol")]
    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
