namespace GeekStore.Identity.Application.Tokens.ViewModels
{
    public class RefreshTokenViewModel
    {
        public Guid Id { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string UserName { get; set; }
    }
}
