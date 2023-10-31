namespace GeekStore.Website.Gateway.WebAPI.Models.Identity
{
    public class TokenResponse
    {
        public string AcessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}
