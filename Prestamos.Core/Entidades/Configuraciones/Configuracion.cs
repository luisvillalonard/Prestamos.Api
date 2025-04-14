using System.ComponentModel.DataAnnotations;

namespace Prestamos.Core.Entidades.Configuraciones;

public partial class Configuracion
{
    [Key]
    public int Id { get; set; }

    public bool PermiteFechaAnteriorHoy { get; set; }
}
