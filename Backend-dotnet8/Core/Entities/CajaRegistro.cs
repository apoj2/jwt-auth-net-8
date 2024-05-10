using Backend_dotnet8.Core.Entities.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_dotnet8.Core.Entities
{
    public class CajaRegistro : BaseEntity<Guid>
    {
        public double DineroApertura { get; set; } = 0.0;

        public double DineroCierre { get; set; } = 0.0;
        public double DiferenciaCierre { get; set; } = 0.0;

        public double Ganacias { get; set; } = 0.0;

        public int TotalFacturasDiarias { get; set; } = 0;

        public TimeSpan HoraApertura { get; set; } = DateTime.Now.TimeOfDay;
        public TimeSpan? HoraCierre { get; set; }

        public Guid IdCaja {  get; set; }
        [ForeignKey("IdCaja")]
        public virtual Caja? IdCajaNavigation { get; set; }



    }
}
