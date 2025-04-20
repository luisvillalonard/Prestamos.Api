using Prestamos.Core.Dto.Clientes;
using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Seguridad;

namespace Prestamos.Core.Dto.Prestamos;

public partial class PrestamoDto
{
    public long Id { get; set; }
    public string Codigo { get; set; } = null!;
    public ClienteDto? Cliente { get; set; }
    public string FechaRegistro { get; set; } = null!;
    public string FechaCredito { get; set; } = null!;
    public FormaPagoDto FormaPago { get; set; } = new();
    public MetodoPagoDto MetodoPago { get; set; } = new();
    public MonedaDto Moneda { get; set; } = new();
    public int Cuotas { get; set; }
    public decimal Monto { get; set; }
    public decimal Interes { get; set; }
    public PrestamoEstadoDto? Estado { get; set; }
    public bool Reenganche { get; set; }
    public bool AplicaDescuento { get; set; }
    public string Destino { get; set; } = null!;
    public AcesorDto? Acesor { get; set; }
    public UsuarioDto? Usuario { get; set; }
    public string? FechaActualizado { get; set; }
    public UsuarioDto? UsuarioIdActualizado { get; set; }
    public bool Cancelado { get; set; }
    public string? FechaCancelado { get; set; }
    public PrestamoCuotaDto[] PrestamoCuotas { get; set; } = [];
}
