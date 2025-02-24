using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prestamos.Core.Entidades.Seguridad;

[Index("RolId", "MenuId", Name = "UN_Permiso_Rol_MenuId", IsUnique = true)]
public partial class Permiso
{
    [Key]
    public int Id { get; set; }

    public int RolId { get; set; }

    public int MenuId { get; set; }

    [ForeignKey("RolId")]
    [InverseProperty("Permiso")]
    public virtual Rol Rol { get; set; } = null!;
}
