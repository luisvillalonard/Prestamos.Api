using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Seguridad;

namespace Prestamos.Core.Dto.Clientes;

public partial class ClienteDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = null!;
    public string EmpleadoId { get; set; } = null!;
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public DocumentoTipoDto DocumentoTipo { get; set; } = null!;
    public string Documento { get; set; } = null!;
    public SexoDto Sexo { get; set; } = null!;
    public string? FechaNacimiento { get; set; }
    public CiudadDto Ciudad { get; set; } = null!;
    public OcupacionDto Ocupacion { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? TelefonoFijo { get; set; }
    public string TelefonoCelular { get; set; } = null!;
    public string? FechaAntiguedad { get; set; }
    public string FechaCreacion { get; set; } = null!;
    public UsuarioDto? Usuario { get; set; } = null!;
    public string? FechaActualizado { get; set; }
    public UsuarioDto? UsuarioActualizado { get; set; }
    public bool Activo { get; set; }
}
