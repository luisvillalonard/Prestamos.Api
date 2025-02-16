using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class DocumentoTipoRepositorio : RepositorioGenerico<DocumentoTipo, int>, IDocumentoTipoRepositorio
    {
        public DocumentoTipoRepositorio(PrestamoContext context) : base(context) { }
    }
}
