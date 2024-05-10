using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Entities;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class NegocioService : GenericoService<Negocio>, INegocioService
    {
        private readonly AppDbContext _conexion;
        public NegocioService(AppDbContext conexion) : base(conexion)
        {
            _conexion = conexion;

        }
    }
}
