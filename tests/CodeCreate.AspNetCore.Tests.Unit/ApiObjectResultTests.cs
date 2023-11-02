using System;
using CodeCreate.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CodeCreate.AspNetCore.Tests.Unit
{
    public class ApiObjectResultTests
    {
        [Fact]
        public void ApiObjectResult_ShouldThrowArgumentNullException_WhenCalledWithNullApiResult()
        {
            // Arrange

            // Act
            var resultAction = () => new ApiObjectResult<string>(null!);

            // Assert
            resultAction
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public void ApiObjectResult_ShouldFillObjectResultDetailsProperly_WhenInitializedThroughConstructorWithASuccessfulApiResult()
        {
            // Arrange
            var successApiResult = ApiResult<string>.CreateSuccessful("data");

            // Act
            var result = new ApiObjectResult<string>(successApiResult);

            // Assert
            result.Value.Should().Be(successApiResult.Data);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public void ApiObjectResult_ShouldFillObjectResultDetailsProperly_WhenInitializedThroughConstructorWithAFailedApiResult()
        {
            // Arrange
            var failedApiResult = ApiResult<string>.CreateFailed(ResultCode.BadGateway, "error", 100, $"{Guid.NewGuid()}");

            // Act
            var result = new ApiObjectResult<string>(failedApiResult);

            // Assert
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
