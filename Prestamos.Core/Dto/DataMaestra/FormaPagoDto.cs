namespace Prestamos.Core.Dto.DataMaestra;

public partial class FormaPagoDto : PropertiesBaseWithActiveDto
{
    public FormaPagoFechaDto[] Dias { get; set; } = [];
}
