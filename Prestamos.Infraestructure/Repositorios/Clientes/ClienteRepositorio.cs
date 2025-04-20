using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Interfaces.Clientes;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Clientes
{
    public class ClienteRepositorio : RepositorioGenerico<Cliente, int>, IClienteRepositorio
    {
        internal IQueryable<Cliente> _dbQuery;
        internal IQueryable<VwCliente> _dbQueryView;
        private readonly IMapper _mapper;

        public ClienteRepositorio(PrestamoContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbQuery = context.Set<Cliente>().AsQueryable<Cliente>();
            _dbQueryView = context.Set<VwCliente>().AsQueryable<VwCliente>();
        }

        public async Task<ResponseResult> Todos(RequestFilter request)
        {
            try
            {
                var filters = ExpressionBuilder.New<VwCliente>();
                if (request is not null && !string.IsNullOrEmpty(request.Filter))
                {
                    filters = item => item.NombreCompleto.ToLower().Contains(request.Filter.ToLower())
                        || item.Codigo.ToLower().Contains(request.Filter.ToLower())
                        || item.Documento.ToLower().Contains(request.Filter.ToLower());
                    _dbQueryView = _dbQueryView.Where(filters);
                }

                if (request is null)
                    request = new();

                var data = _dbQueryView
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
                return await Task.FromResult(new ResponseResult(false, "SItuación inesperada tratando de obtener los datos de los clientes."));
            }
        }

        public async Task<ResponseResult> Activos(RequestFilter request)
        {
            try
            {
                var filters = ExpressionBuilder.New<VwCliente>();
                if (request is not null && !string.IsNullOrEmpty(request.Filter))
                {
                    filters = item => item.Activo == true &
                        (item.NombreCompleto.ToLower().Contains(request.Filter.ToLower())
                        || item.Codigo.ToLower().Contains(request.Filter.ToLower())
                        || item.Documento.ToLower().Contains(request.Filter.ToLower()));
                    _dbQueryView = _dbQueryView.Where(filters);
                }

                if (request is null)
                    request = new();

                var data = _dbQueryView
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
                return await Task.FromResult(new ResponseResult(false, "SItuación inesperada tratando de obtener los datos de los clientes."));
            }
        }

        public override async Task<ResponseResult> PostAsync(Cliente entity)
        {
            // Busco el ultimo cliente registrado
            var ultimo = _dbQuery.OrderByDescending(cli => cli.Id).FirstOrDefault();

            // Obtengo el ultimo codigo de cliente
            int codigo = 1;
            if (ultimo is not null)
                codigo = int.Parse(ultimo.Codigo.Split('-').Last()) + 1;

            // Le asigno el codigo al alicnte
            entity.Codigo = string.Concat("C-", codigo.ToString().PadLeft(5, '0'));

            // Guardo el cliente
            var result = await base.PostAsync(entity);
            
            // Retorno el cliente
            result.Datos = entity;

            // Retorno el resultado
            return result;
        }

        public override async Task<ResponseResult> PutAsync(Cliente entity)
        {
            var result = await base.FindAsync(
                user => user.Id == entity.Id,
                opt => opt.DocumentoTipo, opt => opt.Sexo, opt => opt.Ciudad, opt => opt.Ocupacion, opt => opt.Usuario
            );
            if (!result.Ok)
                return result;

            var cliente = _mapper.Map<IEnumerable<Cliente>>(result.Datos).FirstOrDefault();
            if (cliente == null)
                return new ResponseResult("Código de usuario no encontrado");

            cliente.CiudadId = entity.CiudadId;
            cliente.Direccion = entity.Direccion;
            cliente.TelefonoFijo = entity.TelefonoFijo;
            cliente.TelefonoCelular = entity.TelefonoCelular;
            cliente.UsuarioIdActualizado = entity.UsuarioIdActualizado;
            cliente.FechaActualizado = entity.FechaActualizado;
            cliente.Activo = entity.Activo;
            result = await base.PutAsync(cliente);
            return result;
        }
    }
}
