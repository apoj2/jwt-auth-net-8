using Backend_dotnet8.Core.Constants.Enums;
using Backend_dotnet8.Core.DbContext;
using Backend_dotnet8.Core.Dtos.Entradas;
using Backend_dotnet8.Core.Dtos.Facturas.Entrada;
using Backend_dotnet8.Core.Dtos.Facturas.Salida;
using Backend_dotnet8.Core.Dtos.Salidas;
using Backend_dotnet8.Core.Entities;
using Backend_dotnet8.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IGenericoService<Factura> _service;
        private readonly AppDbContext _conexion;

        public FacturaController(IGenericoService<Factura> service, AppDbContext conexion)
        {
            _service = service;
            _conexion = conexion;

        }
        [HttpGet]
        [Route("facturas-por-caja/{idCajaRegistro}")]
        public async Task<IActionResult> GetFacturasInfoByCajaAsync([FromRoute]Guid idCajaRegistro,[FromQuery]Pagination pagination)
        {
            var facturas = await _conexion.Facturas
                .Where(x=>x.IdCajaRegistro == idCajaRegistro).
                Skip(pagination.offset).
                Take(pagination.limit).ToListAsync();


            List<FacturaInfoSalida> facturaInfoSalida = new List<FacturaInfoSalida>();

            foreach (var factura in facturas)
            {
                var transacciones = await _conexion.Transacciones.Where(x=>x.IdFactura == factura.Id).ToListAsync();
                var facturaInfo = Mapping.GetMapper(factura,transacciones);
                facturaInfoSalida.Add(facturaInfo);
            }
            return Ok(facturaInfoSalida);
        }
        [HttpPost]
        [Route("testiar")]
        public async Task<IActionResult> TestMethods()
        {
            var facturas = await _conexion.Facturas.ToListAsync();


            List<FacturaInfoSalida> facturaInfoSalida = new List<FacturaInfoSalida>();

            foreach (var factura in facturas)
            {
                var transacciones = await _conexion.Transacciones.Where(x => x.IdFactura == factura.Id).ToListAsync();
                var facturaInfo = Mapping.GetMapper(factura, transacciones);
                facturaInfoSalida.Add(facturaInfo);
            }
            return Ok(facturaInfoSalida);
        }
        //public async Task<IEnumerable<FacturaInfoSalida>> GetFacturasListAsync()
        //{
        //   var facturas = await _conexion.Facturas.ToListAsync();


        //List<FacturaInfoSalida> facturaInfoSalida = new List<FacturaInfoSalida>();

        //    foreach (var factura in facturas)
        //    {
        //        var transacciones = await _conexion.Transacciones.Where(x => x.IdFactura == factura.Id).ToListAsync();
        //var facturaInfo = Mapping.GetMapper(factura, transacciones);
        //facturaInfoSalida.Add(facturaInfo);
        //}
        //    return facturaInfoSalida;
        //}
        [HttpPost]
        [Route("Crear")]
        public async Task<Retorno<bool>> Create([FromBody] FacturaEntrada valor)
        {

            try
            {
                Factura entidadMapeada = Mapping.GetMapper(valor);
                if (entidadMapeada != null)
                {
                    bool resultado = await _service.AddAndReturnBoolAsync(entidadMapeada);
                    return new Retorno<bool> { Estado = true, Mensaje = new List<string> { "Consulta Exitosa" }, Informacion = resultado, TipoRetorno = GeneralEnums.TipoRetorno.OK };
                }
                else
                {
                    return new Retorno<bool> { Estado = false, Mensaje = new List<string> { "Haz realizado mal la peticion. ", "Consulta No exitosa" }, Informacion = false, TipoRetorno = GeneralEnums.TipoRetorno.NOK };
                }
                //bool resultado = await _service.AddAndReturnBoolAsync(valor);
                //return new Retorno<bool> { Estado = true, Mensaje = new List<string> { "Consulta Exitosa" }, Informacion = resultado, TipoRetorno = GeneralEnums.TipoRetorno.OK };
            }
            catch (Exception e)
            {

                return new Retorno<bool>() { Mensaje = new List<string>() { "Informacion no satisfactoria ah ocurrido un error ." }, Informacion = false, TipoRetorno = GeneralEnums.TipoRetorno.ERROR, FechaConsulta = DateTime.Now };
            }
            finally
            {

            }
        }
        [HttpPost]
        [Route("Eliminar")]
        public async Task<Retorno<bool>> Delete(Guid id)
        {

            try
            {
                Factura? entidadMapeada = await _service.GetByIdAsync(id);
                if (entidadMapeada != null)
                {
                    bool respuesta = await _service.DeleteAsync(id);
                    return new Retorno<bool> { Estado = true, Mensaje = new List<string> { "Consulta Exitosa" }, Informacion = respuesta, TipoRetorno = GeneralEnums.TipoRetorno.OK };
                }
                else
                {
                    return new Retorno<bool> { Estado = false, Mensaje = new List<string> { "Haz realizado mal la peticion. ", "Consulta No exitosa" }, Informacion = false, TipoRetorno = GeneralEnums.TipoRetorno.NOK };
                }
            }
            catch (Exception e)
            {

                return new Retorno<bool>() { Mensaje = new List<string>() { "Informacion no satisfactoria ah ocurrido un error ." }, Informacion = false, TipoRetorno = GeneralEnums.TipoRetorno.ERROR, FechaConsulta = DateTime.Now };
            }
            finally
            {

            }
        }
    }
}
