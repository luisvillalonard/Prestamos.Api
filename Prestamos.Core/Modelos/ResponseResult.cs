using System;

namespace Prestamos.Core.Modelos
{
    public class ResponseResult : IDisposable
    {
        public bool Ok { get; set; } = true;
        public object? Datos { get; set; } = null;
        public string? Mensaje { get; set; }
        public PagingResult? Paginacion { get; set; } = null;

        public ResponseResult() { }

        public ResponseResult(object datos)
        {
            Datos = datos;
        }

        public ResponseResult(bool ok, string mensaje)
        {
            Ok = ok;
            Mensaje = mensaje;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
