using Backend_dotnet8.Core.Constants;
using Backend_dotnet8.Core.Dtos.Auth;
using Backend_dotnet8.Core.Entities;
using Backend_dotnet8.Core.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend_dotnet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRolesAsync()
        {
            var seedResult = await _service.SeedRolesAsync();
            return StatusCode(seedResult.StatusCode, seedResult.Message);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var registerResult = await _service.RegisterAsync(registerDto);
            return StatusCode(registerResult.StatusCode, registerResult.Message);

        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginServiceResponseDto>> LoginAsync([FromBody] LoginDto loginDto)
        {
            var loginResult = await _service.LoginAsync(loginDto);

            if (loginResult is null)
            {
                return Unauthorized("You credentials are invalid");
            }

            return Ok(loginResult);
        }
        [HttpPost]
        [Route("update-role")]
        [Authorize(Roles = StaticUserRoles.OWNER)]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleDto updateRoleDto)
        {
            var updateRoleResult = await _service.UpdateRoleAsync(User, updateRoleDto);

            if (updateRoleResult.IsSuccess)
            {
                return Ok(updateRoleResult.Message);
            }
            else
            {
                return StatusCode(updateRoleResult.StatusCode, updateRoleResult.Message);
            }
        }
        [HttpPost]
        [Route("me")]
        public async Task<ActionResult<LoginServiceResponseDto>> MeAsync([FromBody] MeDto meDto)
        {
            try
            {
                var me = await _service.MeAsync(meDto);
                if (me is not null)
                {
                    return Ok(me);
                }
                else
                {
                    return Unauthorized("Invalid Token");
                }
            }
            catch (Exception ex)
            {
                return Unauthorized("Invalid Token");
            }
        }
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<UserInfoResult>>> GetUsersListAsync()
        {
            var usersList = await _service.GetUsersListAsync();

            return Ok(usersList);
        }

        [HttpGet]
        [Route("users/{userName}")]
        public async Task<ActionResult<UserInfoResult>> GetUserDetailsByUserNameAsync([FromRoute] string userName)
        {
            var user = await _service.GetUserDetailsByUserNameAsync(userName);
            if (user is not null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound("UserName not found");
            }
        }
        [HttpGet]
        [Route("usernames")]
        public async Task<ActionResult<IEnumerable<string>>> GetUsernamesListAsync()
        {

            var usernames = await _service.GetUsernamesListAsync();
            return Ok(usernames);

        }
    }
}
