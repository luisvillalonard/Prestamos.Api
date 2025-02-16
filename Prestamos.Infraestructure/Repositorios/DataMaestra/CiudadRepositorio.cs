using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class CiudadRepositorio : RepositorioGenerico<Ciudad, int>, ICiudadRepositorio
    {
        public CiudadRepositorio(PrestamoContext context) : base(context) { }
    }
}
