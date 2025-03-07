using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Prestamos
{
    public class PrestamoRepositorio : RepositorioGenerico<Prestamo, long>, IPrestamoRepositorio
    {
        private readonly IMapper _mapper;

        public PrestamoRepositorio(PrestamoContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResponseResult> PostAsync(Prestamo entity)
        {
            // Obtengo el total de prestamos registrados
            var conteo = base.dbQuery.AsNoTracking().AsEnumerable().Count() + 1;

            // Estabezco el código del prestamo
            entity.Codigo = String.Concat("P-", conteo.ToString().PadLeft(6, '0'));

            // Guardo el prestamo
            result = await base.PostAsync(entity);



            // Retorno el resultado
            return result;
        }

        public override async Task<ResponseResult> PutAsync(Prestamo entity)
        {
            var result = await base.PutAsync(entity);
            return result;
        }
    }
}
