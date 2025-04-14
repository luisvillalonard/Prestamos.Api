using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Prestamos
{
    public class PrestamoRepositorio : RepositorioGenerico<Prestamo, long>, IPrestamoRepositorio
    {
        private readonly IPrestamoEstadoRepositorio _repoEstado;
        private readonly IPrestamoCuotaRepositorio _repoCuota;
        private readonly IMapper _mapper;

        public PrestamoRepositorio(
            PrestamoContext context,
            IMapper mapper,
            IPrestamoEstadoRepositorio repoEstado,
            IPrestamoCuotaRepositorio repoCuota) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repoEstado = repoEstado ?? throw new ArgumentNullException(nameof(repoEstado));
            _repoCuota = repoCuota ?? throw new ArgumentNullException(nameof(repoCuota));
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

        public override async Task<ResponseResult> PutAsync(Prestamo entity)
        {
            var result = await base.FindAsync(
                item => item.Id == entity.Id,
                item => item.PrestamoCuota
            );
            if (!result.Ok)
                return result;

            var prestamo = _mapper.Map<IEnumerable<Prestamo>>(result.Datos).FirstOrDefault();
            if (prestamo == null)
                return new ResponseResult("Código de prestamo no encontrado.");

            // Actualizo las cuotas del prestamo
            result = await _repoCuota.PutRangeAsync(prestamo.PrestamoCuota.ToArray());
            if (!result.Ok) 
                return result;

            // Actualizo el usuario y la fecha del prestamo
            prestamo.UsuarioIdActualizado = entity.UsuarioIdActualizado;
            prestamo.FechaActualizado = entity.FechaActualizado;
            result = await base.PutAsync(entity);
            return result;
        }
    }
}
