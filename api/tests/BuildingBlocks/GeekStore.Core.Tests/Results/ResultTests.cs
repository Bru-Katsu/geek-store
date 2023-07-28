using GeekStore.Core.Results;

namespace GeekStore.Core.Tests.Results
{
    public class ResultTests
    {
        [Fact]
        [Trait("SucessResult", "Succeeded")]
        public void ResultFactory_SuccessResult_SucceededShouldBeTrue()
        {
            //Arrange & Act
            Result<int> result = new SuccessResult<int>(10);

            //Assert
            Assert.True(result.Succeeded);
            Assert.Equal(10, result.Data);
        }

        [Fact]
        [Trait("FailResult", "Succeeded")]
        public void ResultFactory_FailResult_SucceededShouldBeFalse()
        {
            //Arrange & Act
            Result<int?> result = new FailResult<int?>();

            //Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
        }
    }
}
