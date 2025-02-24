namespace Prestamos.Core.Dto.Seguridad
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Link { get; set; }
        public int Padre { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public List<MenuDto> Items { get; set; } = new List<MenuDto>();
    }
}
