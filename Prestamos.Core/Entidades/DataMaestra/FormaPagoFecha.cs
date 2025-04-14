using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prestamos.Core.Entidades.DataMaestra;

public partial class FormaPagoFecha
{
    [Key]
    public int Id { get; set; }

    public int FormaPagoId { get; set; }

    public int Dia { get; set; }

    [ForeignKey("FormaPagoId")]
    [InverseProperty("FormaPagoFecha")]
    public virtual FormaPago FormaPago { get; set; } = null!;
}
