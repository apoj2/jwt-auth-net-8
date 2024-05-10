using Backend_dotnet8.Core.Entities.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_dotnet8.Core.Entities
{
    public class Transaccion : BaseEntity<Guid>
    {
        public double Cantidad { get; set; }

        public double TotalMonto { get; set; }

        public Guid IdProducto { get; set; } 

        public Guid IdFactura { get; set; }

        [ForeignKey("IdProducto")]
        public virtual Producto? IdProductoNavigation { get; set; }
        [ForeignKey("IdFactura")]
        public virtual Factura? IdFacturaNavigation { get; set; }

    }
}
