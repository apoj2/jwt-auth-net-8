using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Entities;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class ProductoService : GenericoService<Producto>, IProductoService
    {
        private readonly AppDbContext _conexion;
        public ProductoService(AppDbContext conexion) : base(conexion)
        {
            _conexion = conexion;

        }
    }
}
