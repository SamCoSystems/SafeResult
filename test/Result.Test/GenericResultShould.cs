using Microsoft.AspNetCore.Mvc;
using SamCo.AspNetCore.SafeResult;
using Shouldly;
using Xunit;

namespace SafeResult.Test
{
    public class GenericResultShould
    {
        [Fact]
        public void Give_Item_Back_When_Implicity_Cast()
        {
            // Arrange/Act
            Result<int> five = 5;
            Result<string> hello = "hello";

            // Assert
            five.Errored.ShouldBeFalse();
            five.Value.ShouldBe(5);
            hello.Errored.ShouldBeFalse();
            hello.Value.ShouldBe("hello");
        }

        [Fact]
        public void Give_ActionResult_When_Implicity_Cast()
        {
            // Arrange/Act
            var notFound = new NotFoundResult();
            var forbid = new ForbidResult();
            Result<int> five = notFound;
            Result<string> hello = forbid;

            // Assert
            five.Errored.ShouldBeTrue();
            five.ErrorResult.ShouldBe(notFound);
            hello.Errored.ShouldBeTrue();
            hello.ErrorResult.ShouldBe(forbid);
        }
    }
}