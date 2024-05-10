using Backend_dotnet8.Controllers;
using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Dtos.Auth;
using Backend_dotnet8.Core.Dtos.Facturas.Salida;
using Backend_dotnet8.Core.Entities;
using Backend_dotnet8.Core.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend_dotnet8.Core.Services.Implements
{
    public class FacturaService : GenericoService<Factura>, IFacturaService
    {
        private readonly AppDbContext _conexion;
        public FacturaService(AppDbContext conexion) : base(conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<FacturaInfoSalida>> GetFacturasListAsync(int limit,int offset)
        {
            var facturas = await _conexion.Facturas.Skip(offset).Take(limit).ToListAsync();


            List<FacturaInfoSalida> facturaInfoSalida = new List<FacturaInfoSalida>();

            foreach (var factura in facturas)
            {
                var transacciones = await _conexion.Transacciones.Where(x => x.IdFactura == factura.Id).ToListAsync();
                var facturaInfo = Mapping.GetMapper(factura, transacciones);
                facturaInfoSalida.Add(facturaInfo);
            }
            return facturaInfoSalida;
        }

        //public async Task<UserInfoResult?> GetFacturaDetailsByUserNameAsync(string userName)
        //{
        //    User user = await _conexion.FindByNameAsync(userName);
        //    if (user is null)
        //    {
        //        return null;
        //    }

        //    var roles = await _conexion.GetRolesAsync(user);
        //    var userInfo = GenerateUserInfoObject(user, roles);
        //    return userInfo;
        //}
    }
}
