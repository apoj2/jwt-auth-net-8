namespace Backend_dotnet8.Core.Dtos.Facturas.Entrada
{
    public class FacturaEntrada
    {
        public string NumeroFactura { get; set; } = null!;

        public double TotalMonto { get; set; }
        public double DineroRecibido { get; set; }
        public double Cambio { get; set; }
        public int Items { get; set; }
        public DateTime FechaExpedicion { get; set; }
        public DateTime FechaImpresion { get; set; }
        public string? FormaPago { get; set; }
        public string? IdCajero { get; set; }
        public Guid? IdCajaRegistro { get; set; }
    }
}
