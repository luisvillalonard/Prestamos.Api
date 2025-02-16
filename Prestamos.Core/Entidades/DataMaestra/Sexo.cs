using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Clientes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.DataMaestra;

public partial class Sexo
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Sexo")]
    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();
}
