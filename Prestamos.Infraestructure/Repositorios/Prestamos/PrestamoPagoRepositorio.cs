using AutoMapper;
using Prestamos.Core.Dto.Prestamos;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Prestamos
{
    public class PrestamoPagoRepositorio :
        RepositorioGenerico<PrestamoPago, long>, IPrestamoPagoRepositorio
    {
        private readonly IPrestamoRepositorio _repoPrestamo;
        private readonly IPrestamoCuotaRepositorio _repoCuota;
        private readonly IMapper _mapper;

        public PrestamoPagoRepositorio(
            PrestamoContext context,
            IPrestamoRepositorio repoPrestamo,
            IPrestamoCuotaRepositorio repoCuota,
            IMapper mapper) : base(context)
        {
            _repoPrestamo = repoPrestamo ?? throw new ArgumentNullException(nameof(repoPrestamo));
            _repoCuota = repoCuota ?? throw new ArgumentNullException(nameof(repoCuota));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ResponseResult> PostAsync(PrestamoPago entity, long prestamoId)
        {
            var filters = ExpressionBuilder.New<PrestamoCuota>();
            filters = item => item.PrestamoId == prestamoId;
            filters.And(item => !item.Pagado);

            var result = await _repoCuota.FindAsync(
                item => item.PrestamoId == prestamoId && !item.Pagado,
                opt => opt.OrderBy(ord => ord.Id),
                opt => opt.Prestamo, opt => opt.PrestamoPago);
            if (!result.Ok)
                return result;

            var cuotas = result.Datos as IEnumerable<PrestamoCuota>;
            if (cuotas is null || cuotas.Count() == 0)
                return new ResponseResult(false, "No se encontraron cuotas a las que se le pueda aplicar el pago.");

            decimal montoPagado = entity.Monto;
            List<PrestamoPago> pagos = [];
            List<PrestamoCuota> prestamoCuotas = [];
            foreach (var cuota in cuotas)
            {
                // Si el monto pagado ya esta en cero termino la operacion
                if (montoPagado <= decimal.Zero)
                    break;

                // Establezco el monto a pagar
                decimal montoPagar = ((cuota.Capital - cuota.Descuento) + cuota.Interes) - cuota.PrestamoPago.Sum(p => p.Monto);

                // Genero un pago
                var pago = new PrestamoPago()
                {
                    PrestamoCuotaId = cuota.Id,
                    MetodoPagoId = entity.MetodoPagoId,
                    Monto = montoPagado > montoPagar ? montoPagar : montoPagado,
                    UsuarioId = entity.UsuarioId
                };
                pagos.Add(pago);

                // Si el monto del pago cubre la amortizacion, lo establezco pagado
                if (pago.Monto == montoPagar)
                    cuota.Pagado = true;

                // Agrego la cuota a la lista para actualizarla
                prestamoCuotas.Add(cuota);

                // Descuento el pago actual del monto total pagado
                if (montoPagado > montoPagar)
                    montoPagado = montoPagado - montoPagar;
                else
                    montoPagado = decimal.Zero;
            }

            // Si el monto pagado ya esta en cero termino la operacion
            if (pagos.Count > 0)
            {
                result = await base.PostRangeAsync(pagos.ToArray());
                if (!result.Ok)
                    return result;
            }

            // Actualizo las cuotas
            if (prestamoCuotas.Count > 0)
                result = await _repoCuota.PutRangeAsync(prestamoCuotas.ToArray());

            return new ResponseResult();
        }

        public async Task<ResponseResult> PostAutomaticoAsync(PrestamoPagoDto[] entities)
        {
            var codigos = entities.Where(item => !string.IsNullOrEmpty(item.EmpleadoId))
                .Select(p => p.EmpleadoId ?? string.Empty)
                .Distinct()
                .ToArray();

            var result = await _repoPrestamo.FindAsync(
            item => !item.Estado.Final && codigos.Any(cod => cod == item.Cliente.EmpleadoId),
            item => item.Cliente, item => item.Estado, item => item.MetodoPago, item => item.PrestamoCuota);
            if (!result.Ok)
                return result;

            var prestamos = _mapper.Map<Prestamo[]>(result.Datos);
            if (prestamos.Length == 0)
                return new ResponseResult(false, "No se encontraros prestamos con estos codigos de empleados, a los cuales aplicar estos pagos.");

            List<PrestamoPago> pagos = new();
            List<PrestamoCuota> prestamoCuotas = new();
            foreach (var entity in entities)
            {
                var prestamo = prestamos.FirstOrDefault(p => p.Cliente.EmpleadoId.ToLower().Equals(entity.EmpleadoId?.ToLower()));
                if (prestamo is null)
                {
                    entity.Anulado = true;
                    continue;
                }

                var cuotas = prestamo.PrestamoCuota.Where(cuota => !cuota.Pagado).AsEnumerable();
                if (cuotas is null || cuotas.Count() == 0)
                {
                    entity.Anulado = true;
                    continue;
                }

                decimal montoPagado = entity.Monto;
                foreach (var cuota in cuotas)
                {
                    // Si el monto pagado ya esta en cero termino la operacion
                    if (montoPagado <= decimal.Zero)
                        break;

                    // Establezco el monto a pagar
                    decimal pagosRealizados = pagos.Where(p => p.PrestamoCuotaId == cuota.Id).Sum(p => p.Monto);
                    decimal montoPagar = ((cuota.Capital - cuota.Descuento) + cuota.Interes) - pagosRealizados;

                    // Genero un pago
                    var pago = new PrestamoPago()
                    {
                        PrestamoCuotaId = cuota.Id,
                        MetodoPagoId = prestamo.MetodoPagoId,
                        Monto = montoPagado > montoPagar ? montoPagar : montoPagado,
                        UsuarioId = entity.Usuario is not null ? entity.Usuario.Id : 0,
                    };
                    pagos.Add(pago);

                    // Si el monto del pago cubre la amortizacion de la cuota, la establezco como pagada
                    if (pago.Monto == montoPagar)
                    {
                        cuota.Pagado = true;
                        prestamoCuotas.Add(cuota);
                    }

                    // Descuento el pago actual del monto total pagado
                    if (montoPagado > montoPagar)
                        montoPagado = montoPagado - montoPagar;
                    else
                        montoPagado = decimal.Zero;
                }

            }

            // Si hay pagos los registro
            if (pagos.Count > 0)
            {
                result = await base.PostRangeAsync(pagos.ToArray());
                if (!result.Ok)
                    return new ResponseResult(false, "No fue posible aplicar los pagos a las cuotas correspondientes.");
            }

            // Actualizo las cuotas
            if (prestamoCuotas.Count > 0)
            {
                result = await _repoCuota.PutRangeAsync(prestamoCuotas.ToArray());
                if (!result.Ok)
                    return new ResponseResult(false, "No fue posible actualizar las cuotas de los prestamos correspondientes.");
            }

            return new ResponseResult(entities);
        }
    }
}
