using Microsoft.AspNetCore.Mvc;
using Server.Health;
using Shouldly;
using Xunit;

namespace Unit.Health
{
    public class HealthShould
    {
        private readonly HealthController controller;

        public HealthShould()
        {
            controller = new HealthController();
        }

        [Fact]
        public void ReturnNoContent()
        {
            // Act
            var result = controller.Get();

            // Assert
            result.ShouldBeOfType<NoContentResult>();
        }
    }
}
