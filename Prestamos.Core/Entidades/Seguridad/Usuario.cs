﻿using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.Prestamos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.Seguridad;

public partial class Usuario
{
    [Key]
    public int Id { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string Acceso { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Clave { get; set; } = null!;

    [MaxLength(1024)]
    public byte[] Salt { get; set; } = null!;

    public int RolId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EmpleadoId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Correo { get; set; }

    public bool Cambio { get; set; }

    public bool Activo { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<Cliente> ClienteUsuario { get; set; } = new List<Cliente>();

    [InverseProperty("UsuarioIdActualizadoNavigation")]
    public virtual ICollection<Cliente> ClienteUsuarioIdActualizadoNavigation { get; set; } = new List<Cliente>();

    [InverseProperty("Usuario")]
    public virtual ICollection<PrestamoPago> PrestamoPagoUsuario { get; set; } = new List<PrestamoPago>();

    [InverseProperty("UsuarioIdAnuladoNavigation")]
    public virtual ICollection<PrestamoPago> PrestamoPagoUsuarioIdAnuladoNavigation { get; set; } = new List<PrestamoPago>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Prestamo> PrestamoUsuario { get; set; } = new List<Prestamo>();

    [InverseProperty("UsuarioIdActualizadoNavigation")]
    public virtual ICollection<Prestamo> PrestamoUsuarioIdActualizadoNavigation { get; set; } = new List<Prestamo>();

    [ForeignKey("RolId")]
    [InverseProperty("Usuario")]
    public virtual Rol Rol { get; set; } = null!;
}
