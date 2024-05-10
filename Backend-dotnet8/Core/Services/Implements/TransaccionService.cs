using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Entities;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class TransaccionService : GenericoService<Transaccion>, ITransaccionService
    {
        private readonly AppDbContext _conexion;
        public TransaccionService(AppDbContext conexion) : base(conexion)
        {
            _conexion = conexion;

        }
    }
}
