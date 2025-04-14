using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.DataMaestra;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.DataMaestra
{
    public class FormaPagoRepositorio : RepositorioGenerico<FormaPago, int>, IFormaPagoRepositorio
    {
        private readonly IFormaPagoFechaRepositorio _repoFecha;

        public FormaPagoRepositorio(
            PrestamoContext context,
            IFormaPagoFechaRepositorio repoFecha) : base(context)
        {
            _repoFecha = repoFecha ?? throw new ArgumentNullException(nameof(repoFecha));
        }

        public override async Task<ResponseResult> PostAsync(FormaPago entity)
        {
            // Guardo el prestamo
            result = await base.PostAsync(entity);
            if (!result.Ok)
                return result;

            var cuotas = entity.FormaPagoFecha.Select(fecha => { fecha.FormaPagoId = entity.Id; return fecha; }).ToArray();
            result = await _repoFecha.PostRangeAsync(cuotas);
            if (!result.Ok)
                return result;

            // Retorno el resultado
            return result;
        }

        public override async Task<ResponseResult> PutAsync(FormaPago entity)
        {
            // Guardo el prestamo
            var result = await base.PutAsync(entity);
            if (!result.Ok)
                return result;

            // Elimino las fechas anteriores
            result = await _repoFecha.FindAsync(fecha => fecha.FormaPagoId == entity.Id);
            if (result.Ok && result.Datos is not null)
            {
                var fechasAnteriores = ((IEnumerable<FormaPagoFecha>)result.Datos).ToArray();
                if (fechasAnteriores.Length > 0)
                {
                    result = await _repoFecha.DeleteRangeAsync(fechasAnteriores);
                    if (!result.Ok) 
                        return new ResponseResult(false, "No fue posible actualizar las fechas de la forma de pago.");
                }
            }

            // Agrego las nuevas fechas
            var nuevasFechas = entity.FormaPagoFecha.Select(fecha => { fecha.FormaPagoId = entity.Id; return fecha; }).ToArray();
            result = await _repoFecha.PostRangeAsync(nuevasFechas);
            if (!result.Ok)
                return result;

            // Retorno el resultado
            return result;
        }
    }
}
