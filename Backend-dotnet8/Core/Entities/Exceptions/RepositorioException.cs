namespace Backend_dotnet8.Core.Entities.Exceptions
{
    public class RepositorioException:Exception
    {
        public RepositorioException()
        {
        }

        public RepositorioException(string message)
            : base(message)
        {
        }

        public RepositorioException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
