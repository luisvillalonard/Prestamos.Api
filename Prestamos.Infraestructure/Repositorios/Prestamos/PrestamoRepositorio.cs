using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;
using Prestamos.Infraestructure.TEMP;

namespace Prestamos.Infraestructure.Repositorios.Prestamos
{
    public class PrestamoRepositorio : RepositorioGenerico<Prestamo, long>, IPrestamoRepositorio
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPrestamoEstadoRepositorio _repoEstado;
        private readonly IPrestamoCuotaRepositorio _repoCuota;

        public PrestamoRepositorio(
            PrestamoContext context,
            IMapper mapper,
            IPrestamoEstadoRepositorio repoEstado,
            IPrestamoCuotaRepositorio repoCuota) : base(context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repoEstado = repoEstado ?? throw new ArgumentNullException(nameof(repoEstado));
            _repoCuota = repoCuota ?? throw new ArgumentNullException(nameof(repoCuota));
        }

        public async Task<ResponseResult> Todos(RequestFilter request)
        {
            try
            {
                IQueryable<VwPrestamo> dbQuery = _dbContext.Set<VwPrestamo>().AsQueryable<VwPrestamo>();

                var filters = ExpressionBuilder.New<VwPrestamo>();
                if (request is not null && !string.IsNullOrEmpty(request.Filter))
                {
                    filters = item => item.Codigo.Contains(request.Filter)
                        || item.Cliente.Contains(request.Filter)
                        || item.ClienteCodigo.Contains(request.Filter)
                        || item.ClienteDocumento.Contains(request.Filter);
                }

                if (request is null)
                    request = new();

                var data = dbQuery
                .AsNoTracking()
                .AsEnumerable()
                .Skip((request.CurrentPage - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToArray() ?? [];

                var result = new ResponseResult(data);
                return await Task.FromResult(result);
            }
            catch (Exception)
            {
                return await Task.FromResult(new ResponseResult(false, "SItuación inesperada tratando de obtener los datos de los prestamos."));
            }
        }

        public override async Task<ResponseResult> PostAsync(Prestamo entity)
        {
            // Obtengo el estado del prestamo
            var result = await _repoEstado.GetAllAsync();
            if (!result.Ok || result.Datos is null)
                return new ResponseResult(false, "No fue posible obtener el estado del prestamo.");

            var estado = ((IEnumerable<PrestamoEstado>)result.Datos).FirstOrDefault(est => est.Inicial);
            if (estado is null)
                return new ResponseResult(false, "No fue posible establecer el estado del prestamo.");

            // Establezco el codigo del estado del prestamo
            entity.EstadoId = estado.Id;

            // Obtengo el total de prestamos registrados
            var conteo = base.dbQuery.AsNoTracking().AsEnumerable().Count() + 1;

            // Estabezco el código del prestamo
            entity.Codigo = String.Concat("P-", conteo.ToString().PadLeft(6, '0'));

            // Guardo el prestamo
            result = await base.PostAsync(entity);
            if (!result.Ok)
                return result;

            var cuotas = entity.PrestamoCuota.Select(cuota => { cuota.PrestamoId = entity.Id; return cuota; }).ToArray();
            result = await _repoCuota.PostRangeAsync(cuotas);
            if (!result.Ok)
                return result;

            // Retorno el resultado
            return result;
        }

        public async Task<ResponseResult> ReengancheAsync(Prestamo entity)
        {
            // Obtengo el prestamo anterior
            var result = await base.FindAsync(
                item => !item.Estado.Final & item.ClienteId == entity.ClienteId);
            if (!result.Ok || result.Datos is null)
                return new ResponseResult(false, "No fue posible obtener el prestamo anterior para ser cerrado..");

            Prestamo? prestamo = ((IEnumerable<Prestamo>)result.Datos).FirstOrDefault();
            if (prestamo is null)
                return new ResponseResult(false, "No fue posible establecer el prestamo anterior.");

            // Obtengo el estado final de un prestamo
            result = await _repoEstado.GetAllAsync();
            if (!result.Ok || result.Datos is null)
                return new ResponseResult(false, "No fue posible obtener el estado de cierre para el prestamo anterior.");

            var estado = ((IEnumerable<PrestamoEstado>)result.Datos).FirstOrDefault(item => item.Final);
            if (estado is null)
                return new ResponseResult(false, "No fue posible establecer el estado del prestamo.");

            // Cierro y actualizo el prestamo anterior
            prestamo.EstadoId = estado.Id;
            prestamo.FechaActualizado = DateOnly.FromDateTime(DateTime.Now);
            prestamo.UsuarioIdActualizado = entity.UsuarioId;
            result = await base.PutAsync(prestamo);
            if (!result.Ok)
                return result;

            // Guardo el nuevo prestamo
            result = await PostAsync(entity);
            if (!result.Ok)
                return result;

            // Retorno el resultado
            return result;
        }

        public override async Task<ResponseResult> PutAsync(Prestamo entity)
        {
            var result = await base.FindAsync(item => item.Id == entity.Id);
            if (!result.Ok)
                return result;

            var prestamo = _mapper.Map<IEnumerable<Prestamo>>(result.Datos).FirstOrDefault();
            if (prestamo == null)
                return new ResponseResult("Código de prestamo no encontrado.");

            // Actualizo las cuotas del prestamo
            result = await _repoCuota.PutRangeAsync(entity.PrestamoCuota.ToArray());
            if (!result.Ok) 
                return result;

            // Actualizo el usuario y la fecha del prestamo
            prestamo.UsuarioIdActualizado = entity.UsuarioIdActualizado;
            prestamo.FechaActualizado = entity.FechaActualizado;
            result = await base.PutAsync(prestamo);
            if (!result.Ok)
                return result;
            
            return new ResponseResult();
        }

        public async Task<ResponseResult> Carga(Prestamo[] items)
        {
            var result = await base.PostRangeAsync(items);

            return result;
        }
    }
}
