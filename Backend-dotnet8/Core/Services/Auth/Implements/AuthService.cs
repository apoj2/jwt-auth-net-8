using Backend_dotnet8.Core.Constants;
using Backend_dotnet8.Core.Dtos.Auth;
using Backend_dotnet8.Core.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend_dotnet8.Core.Services.Auth.Implements
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogService _logService;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogService logService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _logService = logService;
        }
        public async Task<GeneralServiceResponseDto> SeedRolesAsync()
        {
            GeneralServiceResponseDto response = new GeneralServiceResponseDto();

            bool isOwnerRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            bool isAdminRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            bool isUserRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);
            bool isManagerRoleExist = await _roleManager.RoleExistsAsync(StaticUserRoles.MANAGER);

            if (isOwnerRoleExist && isAdminRoleExist && isUserRoleExist && isManagerRoleExist)
            {
                response.IsSuccess = true;
                response.StatusCode = 200;
                response.Message = "Roles Seeding is Already Done";
                //return response;

            }
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.OWNER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRoles.MANAGER));

            response.IsSuccess = true;
            response.StatusCode = 201;
            response.Message = "Roles Seeding Done Successfully";
            return response;


        }
        public async Task<GeneralServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            GeneralServiceResponseDto response = new GeneralServiceResponseDto();
            var isExistUser = await _userManager.FindByNameAsync(registerDto.UserName);

            if (isExistUser is not null)
            {
                response.IsSuccess = false;
                response.StatusCode = 409;
                response.Message = "UserName Already Exists";
            }

            User newUser = new User()
            {
                FisrtName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Address = registerDto.Address,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation failed becasue";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += "#" + error.Description;
                }
                response.IsSuccess = false;
                response.StatusCode = 400;
                response.Message = errorString;
            }

            await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);
            await _logService.SaveNewLog(newUser.UserName, "Register");
            response.IsSuccess = true;
            response.StatusCode = 201;
            response.Message = "User created successfully";
            return response;
        }
        public async Task<LoginServiceResponseDto?> LoginAsync(LoginDto loginDto)
        {
            LoginServiceResponseDto response = new LoginServiceResponseDto();

            User user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
            {
                return null;
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
            {
                return null;
            }

            var newToken = await GenerateJWTTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var userInfo = GenerateUserInfoObject(user, roles);

            await _logService.SaveNewLog(user.UserName, "New Login");

            response.userInfo = userInfo;
            response.NewToken = newToken;
            return response;

        }
        public async Task<GeneralServiceResponseDto> UpdateRoleAsync(ClaimsPrincipal user, UpdateRoleDto updateRoleDto)
        {
            GeneralServiceResponseDto response = new GeneralServiceResponseDto();

            User userByName = await _userManager.FindByNameAsync(updateRoleDto.UserName);
            if (userByName is null)
            {
                response.IsSuccess = false;
                response.StatusCode = 404;
                response.Message = "Invalid UserName";
            }

            var userRoles = await _userManager.GetRolesAsync(userByName);

            if (user.IsInRole(StaticUserRoles.ADMIN))
            {
                if (updateRoleDto.NewRole == RoleType.USER || updateRoleDto.NewRole == RoleType.MANAGER)
                {
                    //admin can change the role of everyone except for owners and admins
                    if (userRoles.Any(x => x.Equals(StaticUserRoles.OWNER) || x.Equals(StaticUserRoles.ADMIN)))
                    {
                        response.IsSuccess = false;
                        response.StatusCode = 403;
                        response.Message = "You are not allowed to change of this user";
                    }
                    else
                    {
                        await _userManager.RemoveFromRolesAsync(userByName, userRoles);
                        await _userManager.AddToRoleAsync(userByName, updateRoleDto.NewRole.ToString());
                        await _logService.SaveNewLog(userByName.UserName, "User Roles Update");

                        response.IsSuccess = true;
                        response.StatusCode = 200;
                        response.Message = "Role update successfully";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = 403;
                    response.Message = "You are not allowed to change of this user";
                }
            }
            else
            {
                //user is owner 
                if (userRoles.Any(x => x.Equals(StaticUserRoles.OWNER)))
                {
                    response.IsSuccess = false;
                    response.StatusCode = 403;
                    response.Message = "You are not allowed to change of this user";
                }
                else
                {
                    await _userManager.RemoveFromRolesAsync(userByName, userRoles);
                    await _userManager.AddToRoleAsync(userByName, updateRoleDto.NewRole.ToString());
                    await _logService.SaveNewLog(userByName.UserName, "User Roles Update");

                    response.IsSuccess = true;
                    response.StatusCode = 200;
                    response.Message = "Role update successfully";
                }
            }
            return response;
        }
        public async Task<LoginServiceResponseDto?> MeAsync(MeDto meDto)
        {
            LoginServiceResponseDto response = new LoginServiceResponseDto();

            ClaimsPrincipal handler = new JwtSecurityTokenHandler().ValidateToken(meDto.Token, new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
            }, out SecurityToken securityToken);

            string decodedUserNamer = handler.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            if (decodedUserNamer is null)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(decodedUserNamer);
            if (user == null)
            {
                return null;
            }

            var newToken = await GenerateJWTTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var userInfo = GenerateUserInfoObject(user, roles);

            await _logService.SaveNewLog(user.UserName, "New Token Generated");

            response.NewToken = newToken;
            response.userInfo = userInfo;
            return response;
        }
        public async Task<IEnumerable<UserInfoResult>> GetUsersListAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            List<UserInfoResult> userInfoResult = new List<UserInfoResult>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userInfo = GenerateUserInfoObject(user, roles);
                userInfoResult.Add(userInfo);
            }
            return userInfoResult;
        }
        public async Task<UserInfoResult?> GetUserDetailsByUserNameAsync(string userName)
        {
            User user = await _userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userInfo = GenerateUserInfoObject(user, roles);
            return userInfo;
        }

        public async Task<IEnumerable<string>> GetUsernamesListAsync()
        {
            var userNames = await _userManager.Users
                 .Select(x => x.UserName)
                 .ToListAsync();

            return userNames;
        }
        private async Task<string> GenerateJWTTokenAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("FirstName",user.FisrtName),
                new Claim("LastName",user.LastName),

            };

            foreach (var userRol in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRol));
            }

            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var signInCredentials = new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken
                (
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: signInCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }

        private UserInfoResult GenerateUserInfoObject(User user, IEnumerable<string> roles)
        {
            return new UserInfoResult()
            {
                Id = user.Id,
                FirstName = user.FisrtName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Roles = roles
            };
        }
    }
}
