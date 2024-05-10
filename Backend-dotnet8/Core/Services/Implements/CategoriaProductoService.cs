using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Entities;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class CategoriaProductoService : GenericoService<CategoriaProducto>, ICategoriaProductoService
    {
        private readonly AppDbContext _conexion;
        public CategoriaProductoService(AppDbContext conexion) : base(conexion)
        {
            _conexion = conexion;

        }
    }
}
