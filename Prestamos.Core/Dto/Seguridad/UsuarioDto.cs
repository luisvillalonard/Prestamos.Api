namespace Prestamos.Core.Dto.Seguridad;

public partial class UsuarioDto
{
    public int Id { get; set; }
    public string Acceso { get; set; } = null!;
    public string? EmpleadoId { get; set; }
    public string? Correo { get; set; }
    public bool Cambio { get; set; }
    public bool Activo { get; set; }
}
