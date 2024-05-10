using Backend_dotnet8.Core.Entities.Auth;
using Backend_dotnet8.Core.Entities.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_dotnet8.Core.Entities
{
    public class Factura : BaseEntity<Guid>
    {
        public string NumeroFactura { get; set; } = null!;
        public double TotalMonto {  get; set; } = 0.0;
        public double DineroRecibido { get; set; } = 0.0;
        public double Cambio { get; set; } = 0.0;
        public int Items { get; set; } = 0;
        public DateTime FechaExpedicion { get; set; } = DateTime.Now;
        public DateTime FechaImpresion { get; set; } = DateTime.Now;
        public string? FormaPago { get; set; }
        public string? IdCajero { get; set; }
        public Guid? IdCajaRegistro {  get; set; }

        [ForeignKey("IdCajero")]
        public virtual User? IdUserCajeroNavigation { get; set; }
        [ForeignKey("IdCajaRegistro")]
        public virtual CajaRegistro? IdCajaRegistroNavigation { get; set; }



    }
}
