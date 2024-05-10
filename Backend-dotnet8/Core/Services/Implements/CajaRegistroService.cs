using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Entities;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class CajaRegistroService : GenericoService<CajaRegistro>, ICajaRegistroService
    {
        private readonly AppDbContext _conexion;
        public CajaRegistroService(AppDbContext conexion) : base(conexion)
        {
            _conexion = conexion;

        }

    }
}
