namespace Backend_dotnet8.Core.Dtos.Auth
{
    public class GeneralServiceResponseDto
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }

        public string Message { get; set; }

    }
}
