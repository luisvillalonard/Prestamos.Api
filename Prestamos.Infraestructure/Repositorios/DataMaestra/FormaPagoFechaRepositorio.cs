using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class FormaPagoFechaRepositorio : RepositorioGenerico<FormaPagoFecha, int>, IFormaPagoFechaRepositorio
    {
        public FormaPagoFechaRepositorio(PrestamoContext context) : base(context) { }
    }
}
