﻿namespace Prestamos.Core.Dto.Seguridad
{
    public class RolDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
