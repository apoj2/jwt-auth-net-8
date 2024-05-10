using Backend_dotnet8.Core.Entities.Auth;
using Backend_dotnet8.Core.Entities.Util;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_dotnet8.Core.Entities
{
    public class Negocio : BaseEntity<Guid>
    {
        public string Nombre { get; set; } = null!;

        public string? Nit { get; set; }

        public bool EstaRegistradoCamaraComercio { get; set; } = false;

        public string? Logo { get; set; }
        public double DineroTotal { get; set; } = 0.0;

        public bool TieneLocalFisico { get; set; } = false;
        public string? Direccion { get; set; }

        public string? Barrio { get; set; }

        public string? IdPropietario {  get; set; }
        [ForeignKey("IdPropietario")]
        public virtual User? IdUserPropietarioNavigation {  get; set; }


    }
}
