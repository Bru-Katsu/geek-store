namespace GeekStore.Website.Gateway.WebAPI.Models.Identity
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
