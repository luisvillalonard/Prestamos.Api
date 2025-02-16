namespace Prestamos.Core.Dto.Seguridad
{
    public class LoginDto
    {
        public string Acceso { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool Recuerdame { get; set; }
    }
}
