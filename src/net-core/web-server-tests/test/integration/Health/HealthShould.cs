using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Integration.Health
{
    public class HealthShould : TestBase
    {
        [Fact]
        public async Task ReturnNoContent()
        {
            // Act
            var response = await Client.GetAsync("/health");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }
    }
}
