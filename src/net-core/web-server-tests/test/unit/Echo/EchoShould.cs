using System.Net;
using Microsoft.AspNetCore.Mvc;
using Server.Echo;
using Shouldly;
using Xunit;

namespace Unit.Health
{
    public class EchoShould
    {
        private readonly EchoController controller;

        public EchoShould()
        {
            controller = new EchoController();
        }

        [Fact]
        public void ReturnFooGivenFoo()
        {
            // Act
            var result = controller.Get("foo");

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            okResult.Value.ShouldBe("foo");
        }

        [Fact]
        public void ReturnFooBarGivenFooBar()
        {
            // Act
            var result = controller.Get("foo-bar");

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            okResult.Value.ShouldBe("foo-bar");
        }
    }
}
