using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class AcesorRepositorio : RepositorioGenerico<Acesor, int>, IAcesorRepositorio
    {
        public AcesorRepositorio(PrestamoContext context) : base(context) { }
    }
}
