namespace Backend_dotnet8.Core.Dtos.Transacciones.Entrada
{
    public class TransaccionEntrada
    {
        public double Cantidad { get; set; }

        public double TotalMonto { get; set; }

        public Guid IdProducto { get; set; }

        public Guid IdFactura { get; set; }
    }
}
