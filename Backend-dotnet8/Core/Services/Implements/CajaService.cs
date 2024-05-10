using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Entities;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class CajaService : GenericoService<Caja>, ICajaService
    {
        private readonly AppDbContext _conexion;
        public CajaService(AppDbContext conexion) : base(conexion)
        {
            _conexion = conexion;

        }
    }
}
