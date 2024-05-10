namespace Backend_dotnet8.Core.Dtos.Productos.Entrada
{
    public class ProductoEntrada
    {
        public string Nombre { get; set; } = null!;

        public double PrecioCompra { get; set; }

        public double PrecioVenta { get; set; }

        public double Stock { get; set; }

        public string? Codigo { get; set; }

        public Guid IdCategoriaProducto { get; set; }
    }
}
