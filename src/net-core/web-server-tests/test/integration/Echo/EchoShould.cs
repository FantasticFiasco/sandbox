using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Integration.Echo
{
    public class EchoShould : TestBase
    {
        [Fact]
        public async Task ReturnFooGivenFoo()
        {
            // Act
            var response = await Client.GetAsync("/echo/foo");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            content.ShouldBe("foo");
        }

        [Fact]
        public async Task ReturnFooBarGivenFooBar()
        {
            // Act
            var response = await Client.GetAsync("/echo/foo-bar");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            content.ShouldBe("foo-bar");
        }

        [Fact]
        public async Task ReturnNotFoundGivenNoText()
        {
            // Act
            var response = await Client.GetAsync("/echo");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
