using Backend_dotnet8.Core.Entities.Util;

namespace Backend_dotnet8.Core.Entities
{
    public class Log:BaseEntity<int>
    {
        public string UserName { get; set; }
        public string Description { get; set; }

    }
}
