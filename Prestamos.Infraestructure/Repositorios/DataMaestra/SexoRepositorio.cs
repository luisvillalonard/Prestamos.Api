using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class SexoRepositorio : RepositorioGenerico<Sexo, int>, ISexoRepositorio
    {
        public SexoRepositorio(PrestamoContext context) : base(context) { }
    }
}
