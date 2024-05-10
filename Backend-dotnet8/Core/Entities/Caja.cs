using Backend_dotnet8.Core.Entities.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_dotnet8.Core.Entities
{
    public class Caja : BaseEntity<Guid>
    {
        public string NumeroCaja { get; set; } = null!;

        public Guid? IdNegocio { get; set; }
        [ForeignKey("IdNegocio")]
        public Negocio? IdNegocioNavigation { get; set; }

    }
}
