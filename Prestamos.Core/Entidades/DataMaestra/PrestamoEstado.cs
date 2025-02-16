using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Prestamos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.DataMaestra;

public partial class PrestamoEstado
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("Estado")]
    public virtual ICollection<Prestamo> Prestamo { get; set; } = new List<Prestamo>();
}
