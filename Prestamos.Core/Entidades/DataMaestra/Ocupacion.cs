using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Clientes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.DataMaestra;

public partial class Ocupacion
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    public bool Activo { get; set; }

    [InverseProperty("Ocupacion")]
    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();
}
