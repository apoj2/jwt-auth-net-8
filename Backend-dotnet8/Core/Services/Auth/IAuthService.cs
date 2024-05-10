using Backend_dotnet8.Core.Dtos.Auth;
using System.Security.Claims;

namespace Backend_dotnet8.Core.Services.Auth
{
    public interface IAuthService
    {
        Task<GeneralServiceResponseDto> SeedRolesAsync();

        Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto);

        Task<LoginServiceResponseDto?> LoginAsync(LoginDto loginDto);

        Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal user, UpdateRoleDto updateRoleDto);

        Task<LoginServiceResponseDto?> MeAsync(MeDto meDto);
        Task<IEnumerable<UserInfoResult>> GetUsersListAsync();
        Task<UserInfoResult?> GetUserDetailsByUserNameAsync(string userName);

        Task<IEnumerable<string>> GetUsernamesListAsync();

    }
}
