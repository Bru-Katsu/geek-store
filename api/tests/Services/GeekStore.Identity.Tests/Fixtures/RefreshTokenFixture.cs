namespace GeekStore.Identity.Tests.Fixtures
{
    public class RefreshTokenFixture : ICollectionFixture<RefreshTokenFixture>
    {
        public Domain.Token.RefreshToken CreateValidToken(string email)
        {
            return new Domain.Token.RefreshToken(email, DateTime.Now.AddHours(3));
        }
    }
}
