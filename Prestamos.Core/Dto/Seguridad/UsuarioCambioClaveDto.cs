namespace Prestamos.Core.Dto.Seguridad
{
    public class UsuarioCambioClaveDto
    {
        public int Id { get; set; }
        public string PasswordNew { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
