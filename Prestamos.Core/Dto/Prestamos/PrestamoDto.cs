using Prestamos.Core.Dto.Clientes;
using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Seguridad;

namespace Prestamos.Core.Dto.Prestamos;

public partial class PrestamoDto
{
    public long Id { get; set; }
    public string Codigo { get; set; } = null!;
    public ClienteDto Cliente { get; set; } = null!;
    public string FechaCredito { get; set; } = null!;
    public FormaPagoDto FormaPago { get; set; } = null!;
    public MetodoPagoDto MetodoPago { get; set; } = null!;
    public int Cuotas { get; set; }
    public int FechaCuotas { get; set; }
    public decimal DeudaInicial { get; set; }
    public decimal Interes { get; set; }
    public decimal Capital { get; set; }
    public decimal Amortizacion { get; set; }
    public decimal SaldoFinal { get; set; }
    public decimal AbonoCuota { get; set; }
    public decimal MultaRetrazo { get; set; }
    public string FechaPago { get; set; } = null!;
    public PrestamoEstadoDto Estado { get; set; } = null!;
    public int DiasMora { get; set; }
    public string Destino { get; set; } = null!;
    public UsuarioDto Usuario { get; set; } = null!;
}
