namespace Prestamos.Core.Attributos
{
    public class MenuAttribute : Attribute
    {
        public int Id;
        public string Titulo = null!;
        public string? Link;
        public int Padre;
        public int Orden;
        public string? Icono;
    }
}
