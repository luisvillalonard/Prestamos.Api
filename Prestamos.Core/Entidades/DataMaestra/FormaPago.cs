using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Prestamos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.DataMaestra;

public partial class FormaPago
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [InverseProperty("FormaPago")]
    public virtual ICollection<Prestamo> Prestamo { get; set; } = new List<Prestamo>();

    [InverseProperty("FormaPago")]
    public virtual ICollection<PrestamoPago> PrestamoPago { get; set; } = new List<PrestamoPago>();
}
