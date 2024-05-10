namespace Backend_dotnet8.Core.Dtos.Entradas
{
    public class Pagination
    {
        public int limit { get; set; } = 10;
        public int offset { get; set; } = 0;
    }
}
