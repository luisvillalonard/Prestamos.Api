namespace Prestamos.Core.Dto
{
    public abstract class PropertiesBaseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
