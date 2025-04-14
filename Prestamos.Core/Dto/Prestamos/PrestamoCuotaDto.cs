namespace Prestamos.Core.Dto.Prestamos
{
    public class PrestamoCuotaDto
    {
        public long Id { get; set; }
        public long PrestamoId { get; set; }
        public string FechaPago { get; set; } = null!;
        public decimal DeudaInicial { get; set; }
        public decimal Capital { get; set; }
        public decimal Interes { get; set; }
        public decimal Amortizacion { get; set; }
        public decimal Descuento { get; set; }
        public decimal SaldoFinal { get; set; }
        public bool Vencido { get; set; }
        public bool Pagado { get; set; }
        public PrestamoPagoDto[] Pagos { get; set; } = [];
    }
}
