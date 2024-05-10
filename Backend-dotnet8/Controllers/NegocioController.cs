using Backend_dotnet8.Core.Constants.Enums;
using Backend_dotnet8.Core.Dtos.Negocios.Entrada;
using Backend_dotnet8.Core.Dtos.Salidas;
using Backend_dotnet8.Core.Entities;
using Backend_dotnet8.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegocioController : ControllerBase
    {
        private readonly IGenericoService<Negocio> _service;
        public NegocioController(IGenericoService<Negocio> service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("CrearNegocio")]
        public async Task<Retorno<bool>> Create([FromBody]NegocioEntrada valor)
        {

            try
            {
                Negocio entidadMapeada = Mapping.GetMapper(valor);
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
                Negocio? entidadMapeada = await _service.GetByIdAsync(id);
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
