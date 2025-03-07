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
    public FormaPagoDto FormaPago { get; set; } = null!;
    public MetodoPagoDto MetodoPago { get; set; } = null!;
    public int CantidadCuotas { get; set; }
    public int FechaCuotas { get; set; }
    public decimal DeudaInicial { get; set; }
    public decimal Interes { get; set; }
    public decimal Capital { get; set; }
    public decimal Amortizacion { get; set; }
    public decimal SaldoFinal { get; set; }
    public decimal AbonoCuota { get; set; }
    public decimal MultaRetrazo { get; set; }
    public PrestamoEstadoDto? Estado { get; set; }
    public int DiasMora { get; set; }
    public string Destino { get; set; } = null!;
    public AcesorDto? Acesor { get; set; }
    public UsuarioDto? Usuario { get; set; }
    public string? FechaActualizado { get; set; }
    public UsuarioDto? UsuarioIdActualizado { get; set; }
    public PrestamoCuotaDto[] Cuotas { get; set; } = [];
}
