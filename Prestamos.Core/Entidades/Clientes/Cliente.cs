using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Entidades.Seguridad;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.Clientes;

[Index("Codigo", Name = "IDX_Cliente_Codigo", IsUnique = true)]
public partial class Cliente
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EmpleadoId { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Nombres { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Apellidos { get; set; } = null!;

    public int DocumentoTipoId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Documento { get; set; } = null!;

    public int SexoId { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public int CiudadId { get; set; }

    public int OcupacionId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? TelefonoFijo { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string TelefonoCelular { get; set; } = null!;

    public DateOnly? FechaAntiguedad { get; set; }

    public DateOnly FechaCreacion { get; set; }

    public int UsuarioId { get; set; }

    public DateOnly? FechaActualizado { get; set; }

    public int? UsuarioIdActualizado { get; set; }

    public bool Activo { get; set; }

    [ForeignKey("CiudadId")]
    [InverseProperty("Cliente")]
    public virtual Ciudad Ciudad { get; set; } = null!;

    [ForeignKey("DocumentoTipoId")]
    [InverseProperty("Cliente")]
    public virtual DocumentoTipo DocumentoTipo { get; set; } = null!;

    [ForeignKey("OcupacionId")]
    [InverseProperty("Cliente")]
    public virtual Ocupacion Ocupacion { get; set; } = null!;

    [InverseProperty("Cliente")]
    public virtual ICollection<Prestamo> Prestamo { get; set; } = new List<Prestamo>();

    [ForeignKey("SexoId")]
    [InverseProperty("Cliente")]
    public virtual Sexo Sexo { get; set; } = null!;

    [ForeignKey("UsuarioId")]
    [InverseProperty("ClienteUsuario")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("UsuarioIdActualizado")]
    [InverseProperty("ClienteUsuarioIdActualizadoNavigation")]
    public virtual Usuario? UsuarioIdActualizadoNavigation { get; set; }
}
