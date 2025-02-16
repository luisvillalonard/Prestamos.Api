using AutoMapper;
using Microsoft.AspNetCore.Http;
using Pos.Core.Modelos;
using Prestamos.Core.Dto;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.Seguridad;
using Prestamos.Core.Interfaces.Seguridad;

namespace Prestamos.Infraestructure.Extensiones
{
    public static class HttpRequestExtension
    {
        public static async Task<string?> GetUserCode(this HttpRequest request)
        {
            if (request == null) return null;

            // Obtengo el código de autorización
            var token = request.Headers
                .FirstOrDefault(x => x.Key == nameof(HeaderDto.Authorization)).Value
                .FirstOrDefault();

            if (token == null) return null;

            // Desencripto el token
            var salt = Utileria.GetSalt(Encriptador.key);
            var userCode = Encriptador.AES.Decrypt(token, salt);

            // Si no lo desencripta salgo del proceso
            if (string.IsNullOrEmpty(userCode)) return null;

            // Retorno el usuario
            return await Task.FromResult(userCode);
        }
        
        public static async Task<UserApp?> GetUser(this HttpRequest request)
        {
            var userCode = await GetUserCode(request);
            if (userCode == null)
                return null;

            var userRepositorio = request.HttpContext.RequestServices.GetService(typeof(IUsuarioRepositorio)) as IUsuarioRepositorio;
            if (userRepositorio == null)
                return null;

            var result = await userRepositorio.FindAsync(
                user => user.Acceso.Equals(userCode)
            );
            if (!result.Ok)
                return null;

            var user = (result.Datos as IEnumerable<Usuario>)?.FirstOrDefault();
            if (user == null)
                return null;

            // Establezco el usuario a retornar
            UserApp? userApp = null;
            var mapper = request.HttpContext.RequestServices.GetService(typeof(IMapper));
            if (mapper != null)
                userApp = ((IMapper)mapper).Map<UserApp>(user);

            return userApp;
        }
    }
}
