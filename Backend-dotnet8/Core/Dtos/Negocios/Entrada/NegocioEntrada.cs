namespace Backend_dotnet8.Core.Dtos.Negocios.Entrada
{
    public class NegocioEntrada
    {
        public string Nombre { get; set; } = null!;

        public string? Nit { get; set; }

        public bool EstaRegistradoCamaraComercio { get; set; } = false;

        public string? Logo { get; set; }

        public bool TieneLocalFisico { get; set; } = false;
        public string? Direccion { get; set; }

        public string? Barrio { get; set; }

        public string? IdPropietario { get; set; }
    }
}
