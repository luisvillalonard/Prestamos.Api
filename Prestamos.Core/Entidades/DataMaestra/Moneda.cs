using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Prestamos.Core.Entidades.DataMaestra;

public partial class Moneda
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;
}
