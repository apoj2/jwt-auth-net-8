using Backend_dotnet8.Core.Entities.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_dotnet8.Core.Entities
{
    public class Producto : BaseEntity<Guid>
    {
        public string Nombre { get; set; } = null!;

        public double PrecioCompra {  get; set; }

        public double PrecioVenta { get; set; }

        public double Stock { get; set; }

        public string? Codigo {  get; set; } 

        public Guid IdCategoriaProducto { get; set; }
        [ForeignKey("IdCategoriaProducto")]
        public virtual CategoriaProducto? IdCategoriaProductoNavigation { get; set;}
    }
}
