namespace GeekStore.Identity.Application.Tokens.ViewModels
{
    public class TokenResponseViewModel
    {
        public string AcessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}
