using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pos.Core.Modelos;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Seguridad;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Contexto;
using System.Text;

namespace Prestamos.Infraestructure.Repositorios.Seguridad
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario, int>, IUsuarioRepositorio
    {
        private readonly IMapper _mapper;

        public UsuarioRepositorio(PrestamoContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<ResponseResult> PostAsync(Usuario entity)
        {
            var newPass = Utileria.RandomString(10);
            entity.Salt = Utileria.NewSalt();
            entity.Clave = Encriptador.AES.Encrypt(newPass, entity.Salt);
            entity.Cambio = false;
            entity.Activo = false;

            var result = await base.PostAsync(entity);

            // Envio el correo
            //if (modelo.EnviarCorreo)
            //    _mailService.Correo_Nuevo_Usuario(usuario, newPass);

            return result;
        }

        public override async Task<ResponseResult> PutAsync(Usuario entity)
        {
            var result = await base.FindAsync(user => user.Id == entity.Id, user => user.Rol);
            if (!result.Ok)
                return result;

            var usuario = _mapper.Map<IEnumerable<Usuario>>(result.Datos).FirstOrDefault();
            if (usuario == null)
                return new ResponseResult(false, "Código de usuario no encontrado");

            usuario.EmpleadoId = entity.EmpleadoId;
            usuario.Correo = entity.Correo;
            usuario.Cambio = entity.Cambio;
            usuario.Activo = entity.Activo;

            result = await base.PutAsync(usuario);
            return result;
        }

        public async Task<ResponseResult> ValidarAsync(LoginDto login)
        {
            var usuario = await dbQuery
                .Include(user => user.Rol).ThenInclude(rol => rol.Permiso)
                .FirstOrDefaultAsync(x => x.Acceso.ToLower() == login.Acceso.ToLower());

            if (usuario == null)
                return new ResponseResult(false, "Nombre de usuario no encontrado");

            // Establezco el usuario a retornar
            UserApp userApp = _mapper.Map<UserApp>(usuario);

            // Encripto la clave
            string clave = Encriptador.AES.Decrypt(usuario.Clave, usuario.Salt);

            // Valido la clave del usuario
            if (!login.Clave.Equals(clave, StringComparison.OrdinalIgnoreCase))
                return new ResponseResult(false, "Clave de usuario incorrecta");

            // Agrego el token
            byte[] salt = Utileria.GetSalt(Encriptador.key);
            userApp.Token = Encriptador.AES.Encrypt(usuario.Acceso, salt);

            // Retorno el usuario de la aplicacion
            return new ResponseResult()
            {
                Ok = true,
                Datos = userApp
            };
        }

        public async Task<ResponseResult> PostCambiarClaveAsync(UsuarioCambioClaveDto login)
        {
            if (string.IsNullOrEmpty(login.PasswordNew))
                return new ResponseResult("La nueva clave del usuario es inválida");

            var result = await base.FindAsync(user => user.Id == login.Id);
            if (!result.Ok)
                return result;

            var usuario = _mapper.Map<IEnumerable<Usuario>>(result.Datos).FirstOrDefault();
            if (usuario == null)
                return new ResponseResult("Código de usuario no encontrado");

            usuario.Clave = Encriptador.AES.Encrypt(login.PasswordNew, usuario.Salt);
            usuario.Cambio = true;
            usuario.Activo = true;

            result = await base.PutAsync(usuario);
            if (!result.Ok)
                return result;

            var userApp = _mapper.Map<UserApp>(usuario);

            // Agrego el token
            byte[] salt = Utileria.GetSalt(Encriptador.key);
            userApp.Token = Encriptador.AES.Encrypt(usuario.Acceso, salt);

            // Retorno el token generado
            return new ResponseResult()
            {
                Ok = true,
                Datos = userApp
            };
        }
    }
}
