namespace Prestamos.Core.Dto.Seguridad
{
    public class UserApp : UsuarioDto
    {
        public string Token { get; set; } = null!;
    }
}
