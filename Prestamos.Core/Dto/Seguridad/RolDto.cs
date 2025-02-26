namespace Prestamos.Core.Dto.Seguridad
{
    public class RolDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool EsAdmin { get; set; }
        public bool Activo { get; set; }
        public PermisoDto[] Permisos { get; set; } = Array.Empty<PermisoDto>();
    }
}
