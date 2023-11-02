using System;
using FluentAssertions;
using Xunit;

namespace CodeCreate.Core.Tests.Unit
{
    public class ApiResultTests
    {
        [Fact]
        public void CreateSuccessful_ShouldReturnSuccessApiResult_WhenCalled()
        {
            // Arrange

            // Act
            var result = ApiResult<string>.CreateSuccessful("data");

            // Assert
            result.Successful.Should().BeTrue();
            result.ErrorText.Should().BeNull();
            result.Data.Should().BeOfType<string>();
            result.Code.Should().Be(ResultCode.Success);
        }

        [Fact]
        public void CreateFailed_FirstOverload_ShouldThrowArgumentOutOfRangeException_WhenCalledWithInvalidResultCode()
        {
            // Arrange

            // Act
            var resultAction = () => ApiResult<string>.CreateFailed(ResultCode.Success, "error");

            // Assert
            resultAction
                .Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CreateFailed_FirstOverload_ShouldReturnFailedApiResult_WhenCalled()
        {
            // Arrange

            // Act
            var result = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error", 100, $"{Guid.NewGuid()}");

            // Assert
            result.Successful.Should().BeFalse();
            result.EventId.Should().Be(100);
            result.ErrorText.Should().Be("error");
            result.Code.Should().Be(ResultCode.BadGateway);
        }

        [Fact]
        public void CreateFailed_SecondOverload_ShouldReturnFailedApiResult_WhenCalled()
        {
            // Arrange

            // Act
            var result = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error");

            // Assert
            result.Successful.Should().BeFalse();
            result.ErrorText.Should().Be("error");
            result.Code.Should().Be(ResultCode.BadGateway);
        }

        [Fact]
        public void CreateFailed_ThirdOverload_ShouldReturnFailedApiResult_WhenCalled()
        {
            // Arrange
            var failedApiResult = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error");

            // Act
            var result = ApiResult<string>.CreateFailed(failedApiResult);

            // Assert
            result.Should().BeEquivalentTo(failedApiResult);
        }
    }
}
