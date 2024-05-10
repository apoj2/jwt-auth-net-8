using Backend_dotnet8.Core.Dtos.Log;
using System.Security.Claims;

namespace Backend_dotnet8.Core.Services
{
    public interface ILogService
    {
        Task SaveNewLog(string userName, string description);

        Task<IEnumerable<GetLogDto>> GetLogAsync();

        Task<IEnumerable<GetLogDto>> GetMyLogsAsync(ClaimsPrincipal user);
    }
}
