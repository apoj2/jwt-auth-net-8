namespace Backend_dotnet8.Core.Dtos.Auth
{
    public class LoginServiceResponseDto
    {
        public string NewToken { get; set; }
        //this would be returned to front end
        public UserInfoResult userInfo {  get; set; }
    }
}
