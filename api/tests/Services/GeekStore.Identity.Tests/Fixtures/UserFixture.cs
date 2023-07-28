using Bogus;
using GeekStore.Identity.Tests.Common;

namespace GeekStore.Identity.Tests.Fixtures
{
    public class UserFixture : ICollectionFixture<UserFixture>
    {
        public Domain.User.User CreateValidUser()
        {
            return new Faker<Domain.User.User>(Constants.LOCALE)
            .CustomInstantiator(f =>
            {
                return new Domain.User.User(f.Internet.Email())
                {
                    UserName = f.Internet.UserName(),
                    PhoneNumber = f.Phone.PhoneNumber(),
                };                
            });
        }
    }
}
