using Microsoft.AspNetCore.Mvc;
using SamCo.AspNetCore.SafeResult;
using Shouldly;
using Xunit;

namespace SafeResult.Test
{
    public class ResultShould
    {
        [Fact]
        public void Give_ActionResult_Back_When_Implicity_Cast()
        {
            // Arrange
            var notFound = new NotFoundResult();

            // Act
            Result result = notFound;

            // Assert
            result.Errored.ShouldBeTrue();
            result.ErrorResult.ShouldBe(notFound);
        }

        [Fact]
        public void Not_Be_Errored_When_Created_So()
        {
            // Arrange/Act
            Result result = Result.WithoutError;

            // Assert
            result.Errored.ShouldBeFalse();
        }
    }
}
