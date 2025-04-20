using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Prestamos.Core.Entidades.Clientes;

[Keyless]
public partial class VwCliente
{
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string EmpleadoId { get; set; } = null!;

    [StringLength(301)]
    [Unicode(false)]
    public string NombreCompleto { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string DocumentoTipo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Documento { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Sexo { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Ciudad { get; set; } = null!;

    [StringLength(150)]
    [Unicode(false)]
    public string Ocupacion { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string TelefonoCelular { get; set; } = null!;

    public bool Activo { get; set; }
}
