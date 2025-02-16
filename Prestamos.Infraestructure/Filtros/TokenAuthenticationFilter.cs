using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Prestamos.Core.Dto;
using Prestamos.Core.Modelos;
using Prestamos.Infraestructure.Extensiones;

namespace Prestamos.Infraestructure.Filtros
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null)
                return;

            var allowAnonymous = descriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            if (descriptor == null)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseResult(false, "No fue posible establecer la ruta solicitada."));
                return;
            }

            var user = context.HttpContext.Request.GetUser();
            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult(new ResponseResult(false, "Token de usuario inválido."));
                return;
            }

            //var headers = context.HttpContext.Request.Headers;
            //if (headers == null)
            //    return;

            //if (!headers.ContainsKey(nameof(HeaderDto.Authorization)))
            //{
            //    context.Result = new UnauthorizedObjectResult(new ResponseResult(false, "Token de autorización no encontrado."));
            //    return;
            //}

            //string token = headers[nameof(HeaderDto.Authorization)]!;
            //if (string.IsNullOrEmpty(token))
            //{
            //    context.Result = new UnauthorizedObjectResult(new ResponseResult(false, "Token de autorización inválido."));
            //    return;
            //}

            //token = Encriptador.AES.Decrypt(token, Encoding.UTF8.GetBytes(Encriptador.key));
            //if (token == null)
            //{
            //    context.Result = new UnauthorizedObjectResult(new ResponseResult(false, "Token data de autorización inválido."));
            //    return;
            //}

            //var guidToken = Guid.Parse(token);
            //var userRepositorio = context.HttpContext.RequestServices.GetService(typeof(IUsuarioRepositorio)) as IUsuarioRepositorio;
            //if (userRepositorio == null)
            //    return;

            //var result = userRepositorio.FindAsync(user => user.Codigo.Equals(token), user => user.Empresa).Result;
            //if (!result.Ok)
            //{
            //    context.Result = new UnauthorizedObjectResult(new ResponseResult(false, "Token de usuario inválido."));
            //    return;
            //}

            //var user = (result.Datos as IEnumerable<Usuario>)?.FirstOrDefault();
            //if (user == null)
            //{
            //    context.Result = new UnauthorizedObjectResult(new ResponseResult(false, "Token de usuario inválido."));
            //    return;
            //}
        }
    }
}
