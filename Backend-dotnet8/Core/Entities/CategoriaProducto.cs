using Backend_dotnet8.Core.Entities.Util;

namespace Backend_dotnet8.Core.Entities
{
    public class CategoriaProducto:BaseEntity<Guid>
    {

        public string Descripcion { get; set; } = null!;

    }
}
