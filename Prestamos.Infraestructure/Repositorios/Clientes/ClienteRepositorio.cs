using AutoMapper;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Clientes;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;

namespace Prestamos.Infraestructure.Repositorios.Clientes
{
    public class ClienteRepositorio : RepositorioGenerico<Cliente, int>, IClienteRepositorio
    {
        internal IQueryable<Cliente> _dbQuery;
        private readonly IMapper _mapper;

        public ClienteRepositorio(PrestamoContext context, IMapper mapper) : base(context)
        {
            _dbQuery = context.Set<Cliente>().AsQueryable<Cliente>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResponseResult> PostAsync(Cliente entity)
        {
            // Busco el ultimo cliente registrado
            var ultimo = _dbQuery.OrderByDescending(cli => cli.Id).FirstOrDefault();

            // Obtengo el ultimo codigo de cliente
            int codigo = ultimo is not null
                ? int.Parse(ultimo.Codigo.Split(',').Last()) + 1
                : 1;

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
