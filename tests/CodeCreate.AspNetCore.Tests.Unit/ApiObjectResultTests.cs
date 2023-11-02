using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using Xunit;
using CodeCreate.Core;

namespace CodeCreate.AspNetCore.Tests.Unit
{
    public class ApiObjectResultTests
    {
        [Fact]
        public void ApiObjectResult_ShouldThrowArgumentNullException_WhenCalledWithNullApiResult()
        {
            var resultAction = () => new ApiObjectResult<string>(null!);

            resultAction
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public void ApiObjectResult_ShouldFillObjectResultDetailsProperly_WhenInitializedThroughConstructorWithASuccessfulApiResult()
        {
            var successApiResult = ApiResult<string>.CreateSuccessful("data");

            var result = new ApiObjectResult<string>(successApiResult);

            result.Value.Should().Be(successApiResult.Data);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void ApiObjectResult_ShouldFillObjectResultDetailsProperly_WhenInitializedThroughConstructorWithAFailedApiResult()
        {
            var failedApiResult = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error", 
                100, $"{Guid.NewGuid()}");

            var result = new ApiObjectResult<string>(failedApiResult);

            result.Value.Should().BeEquivalentTo(
                new 
                {
                    status = failedApiResult.Code,
                    eventId = failedApiResult.EventId,
                    message = failedApiResult.ErrorText
                }
            );
            result.StatusCode.Should().Be(failedApiResult.Code);
        }
    }
}
