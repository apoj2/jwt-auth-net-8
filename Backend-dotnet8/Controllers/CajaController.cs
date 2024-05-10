using Backend_dotnet8.Core.Constants.Enums;
using Backend_dotnet8.Core.Dtos.Cajas.Entrada;
using Backend_dotnet8.Core.Dtos.Negocios.Entrada;
using Backend_dotnet8.Core.Dtos.Salidas;
using Backend_dotnet8.Core.Entities;
using Backend_dotnet8.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Backend_dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajaController : ControllerBase
    {
        private readonly IGenericoService<Caja> _service;
        public CajaController(IGenericoService<Caja> service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("Crear")]
        public async Task<Retorno<bool>> Create([FromBody] CajaEntrada valor)
        {

            try
            {
                Caja entidadMapeada = Mapping.GetMapper(valor);
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
                Caja? entidadMapeada = await _service.GetByIdAsync(id);
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


        //public async Task<Retorno<BarrioAdministradorSalidaDto>> GetById(Guid id)
        //{

        //    try
        //    {
        //        Barrio resultado = await _service.GetByIdAsync(id);

        //        if (resultado != null)
        //        {
        //            BarrioAdministradorSalidaDto resultadoSalida = Mapping.GetMapper(resultado);

        //            return new Retorno<BarrioAdministradorSalidaDto> { Estado = true, Mensaje = new List<string> { "Consulta Exitosa" }, Informacion = resultadoSalida, TipoRetorno = GeneralEnum.TipoRetorno.OK };
        //        }
        //        else
        //        {
        //            return new Retorno<BarrioAdministradorSalidaDto> { Estado = false, Mensaje = new List<string> { "Haz realizado mal la peticion. ", "Consulta No exitosa" }, Informacion = null, TipoRetorno = GeneralEnum.TipoRetorno.NOK };
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        return new Retorno<BarrioAdministradorSalidaDto>() { Mensaje = new List<string>() { "Informacion no satisfactoria ah ocurrido un error ." }, Informacion = null!, TipoRetorno = GeneralEnum.TipoRetorno.ERROR, FechaConsulta = DateTime.Now };
        //    }
        //    finally
        //    {

        //    }
        //}

        //public async Task<Retorno<bool>> Update(Guid id, BarrioAdministradorEntradaDto valor)
        //{

        //    try
        //    {
        //        Barrio resultado = await _repositorio.GetByIdAsync(id);
        //        if (resultado != null)
        //        {
        //            Barrio resultadoSalida = Mapping.GetMapper(id, valor);

        //            await _repositorio.UpdateAsync(resultadoSalida);
        //            return new Retorno<bool> { Estado = true, Mensaje = new List<string> { "Consulta Exitosa" }, Informacion = true, TipoRetorno = GeneralEnum.TipoRetorno.OK };
        //        }
        //        else
        //        {
        //            return new Retorno<bool> { Estado = false, Mensaje = new List<string> { "Haz realizado mal la peticion. ", "Consulta No exitosa" }, Informacion = false, TipoRetorno = GeneralEnum.TipoRetorno.NOK };
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        return new Retorno<bool>() { Mensaje = new List<string>() { "Informacion no satisfactoria ah ocurrido un error ." }, Informacion = false, TipoRetorno = GeneralEnum.TipoRetorno.ERROR, FechaConsulta = DateTime.Now };
        //    }
        //    finally
        //    {

        //    }
        //}
    }
}
