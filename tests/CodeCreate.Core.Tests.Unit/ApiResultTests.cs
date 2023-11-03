using FluentAssertions;
using System;
using Xunit;

namespace CodeCreate.Core.Tests.Unit
{
    public class ApiResultTests
    {
        [Fact]
        public void CreateSuccessful_ShouldReturnSuccessApiResult_WhenCalled()
        {
            var data = "data";

            var result = ApiResult<string>.CreateSuccessful(data);

            result.Successful.Should().BeTrue();
            result.ErrorText.Should().BeNull();
            result.Data.Should().Be(data);
            result.Code.Should().Be(ResultCode.Success);
        }

        [Fact]
        public void CreateFailed_ShouldThrowArgumentOutOfRangeException_WhenCalledWithInvalidResultCode()
        {
            var resultAction = 
                () => ApiResult<string>.CreateFailed(ResultCode.Success, "error");

            resultAction
                .Should()
                .Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void CreateFailed_ShouldReturnFailedApiResult_WhenCalledProperly()
        {
            var result = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error", 
                100, $"{Guid.NewGuid()}");

            result.Successful.Should().BeFalse();
            result.EventId.Should().Be(100);
            result.ErrorText.Should().Be("error2");
            result.Code.Should().Be(ResultCode.BadGateway);
        }

        [Fact]
        public void CreateFailed_SecondOverload_ShouldReturnFailedApiResult_WhenCalled()
        {
            var result = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error");

            result.Successful.Should().BeFalse();
            result.ErrorText.Should().Be("error");
            result.Code.Should().Be(ResultCode.BadGateway);
        }

        [Fact]
        public void CreateFailed_ThirdOverload_ShouldReturnFailedApiResult_WhenCalled()
        {
            var failedApiResult = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error");

            var result = ApiResult<string>.CreateFailed(failedApiResult);

            result.Should().BeEquivalentTo(failedApiResult);
        }
    }
}
