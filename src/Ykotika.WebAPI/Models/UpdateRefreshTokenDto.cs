namespace Ykotika.WebAPI.Models
{
    public class UpdateRefreshTokenDto
    {
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
