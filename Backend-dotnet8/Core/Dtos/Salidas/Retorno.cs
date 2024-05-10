using Backend_dotnet8.Core.Constants.Enums;
using System.Net;

namespace Backend_dotnet8.Core.Dtos.Salidas
{
    public class Retorno<T>
    {
        public Retorno()
        {
            Mensaje = new List<string>();

        }

        public bool Estado { get; set; }

        public T? Informacion { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public GeneralEnums.TipoRetorno TipoRetorno { get; set; }

        public DateTime FechaConsulta { get; set; } = DateTime.UtcNow;

        public List<string> Mensaje { get; set; }

    }
}