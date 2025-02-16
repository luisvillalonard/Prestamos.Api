using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class FormaPagoRepositorio : RepositorioGenerico<FormaPago, int>, IFormaPagoRepositorio
    {
        public FormaPagoRepositorio(PrestamoContext context) : base(context) { }
    }
}
