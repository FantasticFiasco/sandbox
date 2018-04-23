using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Unit.Contacts
{
    public class DeleteContactShould
    {
        private readonly Mock<IContactService> contactService;
        private readonly ContactsController controller;

        public DeleteContactShould()
        {
            contactService = new Mock<IContactService>();
            controller = new ContactsController(contactService.Object);
        }

        [Fact]
        public void ReturnNoContent()
        {
            // Arrange
            contactService
                .Setup(mock => mock.Remove(42))
                .Returns(true);

            // Act
            var result = controller.Delete(42);

            // Assert
            result.ShouldBeOfType<NoContentResult>();
        }

        [Fact]
        public void ReturnNotFoundGivenUnknownId()
        {
            // Arrange
            contactService
                .Setup(mock => mock.Remove(42))
                .Returns(false);

            // Act
            var result = controller.Delete(42);

            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
