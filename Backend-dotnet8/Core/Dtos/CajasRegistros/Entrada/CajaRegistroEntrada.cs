namespace Backend_dotnet8.Core.Dtos.CajasRegistros.Entrada
{
    public class CajaRegistroEntrada
    {
        public double DineroApertura { get; set; }

        public double DineroCierre { get; set; }
        public double DiferenciaCierre { get; set; } = 0.0;

        public double Ganacias { get; set; }

        public int TotalFacturasDiarias { get; set; }

        public TimeSpan HoraApertura { get; set; }
        public TimeSpan? HoraCierre { get; set; }

        public Guid IdCaja { get; set; }
    }
}
