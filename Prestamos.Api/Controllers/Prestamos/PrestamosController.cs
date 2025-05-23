﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prestamos.Core.Dto.Clientes;
using Prestamos.Core.Dto.Prestamos;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Interfaces.Prestamos;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Extensiones;

namespace Prestamos.Api.Controllers.Prestamos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPrestamoRepositorio _repositorio;
        private readonly IPrestamoPagoRepositorio _repoPago;

        public PrestamosController(
            IMapper mapper,
            IPrestamoRepositorio repositorio,
            IPrestamoPagoRepositorio repoPago)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _repoPago = repoPago ?? throw new ArgumentNullException(nameof(repoPago));
        }

        [HttpGet("todos")]
        public async Task<IActionResult> Get([FromQuery] RequestFilter request)
        {
            var result = await _repositorio.Todos(request);
            return Ok(result);
        }

        [HttpGet("activos")]
        public async Task<IActionResult> GetActivos([FromQuery] RequestFilter request)
        {
            var filters = ExpressionBuilder.New<Prestamo>();
            filters = item => !item.Estado.Final;

            if (request is not null && !string.IsNullOrEmpty(request.Filter))
            {
                filters.And(item => item.Codigo.Contains(request.Filter)
                    || item.Cliente.Nombres.Contains(request.Filter)
                    || item.Cliente.Apellidos.Contains(request.Filter)
                    || item.Cliente.Codigo.Contains(request.Filter)
                    || item.Cliente.Documento.Contains(request.Filter));
            }

            var result = await _repositorio.FindAndPagingAsync(
                request ?? new(),
                filters,
                opt => opt.OrderByDescending(ord => ord.Id),
                opt => opt.Cliente, opt => opt.Cliente.DocumentoTipo, opt => opt.Cliente.Sexo, opt => opt.Cliente.Ciudad, opt => opt.Cliente.Ocupacion, opt => opt.Estado, opt => opt.FormaPago, opt => opt.MetodoPago, opt => opt.Moneda, opt => opt.Acesor!, opt => opt.PrestamoCuota);
            if (!result.Ok)
                return Ok(result);

            var prestamos = _mapper.Map<IEnumerable<PrestamoDto>>(result.Datos);
            result.Datos = await CargarPagos(prestamos);
            return Ok(result);
        }

        [HttpGet("actual")]
        public async Task<IActionResult> GetPorCliente([FromQuery] int id)
        {
            var result = await _repositorio.FindAsync(
                item => !item.Estado.Final & item.ClienteId == id,
                opt => opt.OrderByDescending(ord => ord.Id),
                opt => opt.Cliente, opt => opt.Cliente.DocumentoTipo, opt => opt.Cliente.Sexo, opt => opt.Cliente.Ciudad, opt => opt.Cliente.Ocupacion, opt => opt.Estado, opt => opt.FormaPago, opt => opt.FormaPago.FormaPagoFecha, opt => opt.MetodoPago, opt => opt.Moneda, opt => opt.Acesor!, opt => opt.PrestamoCuota);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<PrestamoDto>>(result.Datos).FirstOrDefault();
            return Ok(result);
        }

        [HttpGet("porId")]
        public async Task<IActionResult> GetById([FromQuery] long id)
        {
            var result = await _repositorio.FindAsync(
                item => item.Id == id,
                opt => opt.Cliente, opt => opt.Cliente.DocumentoTipo, opt => opt.Cliente.Sexo, opt => opt.Cliente.Ciudad, opt => opt.Cliente.Ocupacion, opt => opt.Estado, opt => opt.FormaPago, opt => opt.FormaPago.FormaPagoFecha, opt => opt.MetodoPago, opt => opt.Moneda, opt => opt.Acesor!, opt => opt.PrestamoCuota);
            if (!result.Ok)
                return Ok(result);

            var prestamo = _mapper.Map<IEnumerable<PrestamoDto>>(result.Datos).FirstOrDefault();
            if (prestamo is not null)
            {
                var prestamos = await CargarPagos([prestamo]);
                prestamo = prestamos.FirstOrDefault();
            }

            result.Datos = prestamo;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PrestamoDto modelo)
        {
            var user = await Request.GetUser();
            if (user == null)
                return Ok(new ResponseResult(false, "Código de usuario inválido"));

            var item = _mapper.Map<Prestamo>(modelo);
            item.UsuarioId = user.Id;

            ResponseResult result = new();
            if (modelo.Reenganche)
                result = await _repositorio.ReengancheAsync(item);
            else
                result = await _repositorio.PostAsync(item);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PrestamoDto modelo)
        {
            var user = await Request.GetUser();
            if (user == null)
                return Ok(new ResponseResult(false, "Código de usuario inválido"));

            var item = _mapper.Map<Prestamo>(modelo);
            item.UsuarioIdActualizado = user.Id;
            item.FechaActualizado = DateOnly.FromDateTime(DateTime.Now);
            var result = await _repositorio.PutAsync(item);
            return Ok(result);
        }

        private async Task<IEnumerable<PrestamoDto>> CargarPagos(IEnumerable<PrestamoDto> prestamos)
        {
            try
            {
                List<long> cuotasIds = [];
                foreach (var prestamo in prestamos)
                    cuotasIds.AddRange(prestamo.PrestamoCuotas.Select(c => c.Id).ToList());

                var result = await _repoPago.FindAsync(
                    opt => cuotasIds.Any(valor => valor == opt.PrestamoCuotaId),
                    opt => opt.MetodoPago);
                if (result.Ok)
                {
                    var pagos = _mapper.Map<IEnumerable<PrestamoPagoDto>>(result.Datos);
                    if (pagos != null)
                        foreach (var prestamo in prestamos)
                            foreach (var cuota in prestamo.PrestamoCuotas)
                                cuota.Pagos = pagos.Where(p => p.PrestamoCuotaId == cuota.Id).ToArray();
                }
            }
            catch (Exception)
            {
                return [];
            }

            return prestamos;
        }

        [HttpPost("carga")]
        public async Task<IActionResult> Post([FromBody] PrestamoDto[] items)
        {
            var user = await Request.GetUser();
            if (user == null)
                return Ok(new ResponseResult(false, "Código de usuario inválido"));

            IEnumerable<Prestamo> prestamos = Enumerable.Empty<Prestamo>();

            try
            {
                prestamos = _mapper.Map<IEnumerable<Prestamo>>(items);
            }
            catch (Exception)
            {
                return Ok(new ResponseResult(false, "Situación inesperada tratando de establecer los prestamos que se van a procesar."));
            }

            if (!prestamos.Any())
                return Ok(new ResponseResult(false, "No fue posible establecer los prestamos que se van a procesar"));

            prestamos = prestamos.Select(cli => { cli.UsuarioId = user.Id; return cli; });
            var result = await _repositorio.Carga(prestamos.ToArray());
            return Ok(result);
        }
    }
}
