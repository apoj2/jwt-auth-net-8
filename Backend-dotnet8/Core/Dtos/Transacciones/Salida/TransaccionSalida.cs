namespace Backend_dotnet8.Core.Dtos.Transacciones.Salida
{
    public class TransaccionSalida
    {
        public double Cantidad { get; set; }

        public double TotalMonto { get; set; }

        public Guid IdProducto { get; set; }

        public Guid IdFactura { get; set; }
    }
}
