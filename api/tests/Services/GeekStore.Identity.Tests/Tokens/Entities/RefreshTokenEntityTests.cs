using GeekStore.Identity.Domain.Token;

namespace GeekStore.Identity.Tests.Tokens.Entities
{
    public class RefreshTokenEntityTests
    {
        [Fact(DisplayName = "Ao criar um refresh token válido, método deve retornar válido")]
        [Trait("Entities", "RefreshToken")]
        public void RefreshToken_IsValid_ShouldBeValid()
        {
            //arrange
            var entity = new RefreshToken("teste", DateTime.Now.AddHours(1));

            //act
            var result = entity.IsValid();

            //assert
            Assert.True(result);
            Assert.False(entity.ValidationResult.Errors.Any());
        }

        [Fact(DisplayName = "Ao criar um refresh token com data retroativa, método deve retornar inválido")]
        [Trait("Entities", "RefreshToken")]
        public void RefreshToken_RetroactiveExpiration_ShouldBeInvalid()
        {
            //arrange
            var entity = new RefreshToken("teste", DateTime.Now.AddHours(-1));

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Contains(nameof(RefreshToken.ExpirationDate), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um refresh token com data nula, método deve retornar inválido")]
        [Trait("Entities", "RefreshToken")]
        public void RefreshToken_NullDate_ShouldBeInvalid()
        {
            //arrange
            var entity = new RefreshToken("teste", default);

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Contains(nameof(RefreshToken.ExpirationDate), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }

        [Fact(DisplayName = "Ao criar um refresh token com username vazio, método deve retornar inválido")]
        [Trait("Entities", "RefreshToken")]
        public void RefreshToken_UsernameEmpty_ShouldBeInvalid()
        {
            //arrange
            var entity = new RefreshToken(string.Empty, DateTime.Now.AddHours(1));

            //act
            var result = entity.IsValid();

            //assert
            Assert.False(result);
            Assert.Contains(nameof(RefreshToken.UserName), entity.ValidationResult.Errors.Select(x => x.PropertyName));
        }
    }
}
