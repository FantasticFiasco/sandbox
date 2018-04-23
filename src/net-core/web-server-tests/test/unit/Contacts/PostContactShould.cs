using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Unit.Contacts
{
    public class PostContactShould
    {
        private readonly Mock<IContactService> contactService;
        private readonly ContactsController controller;

        public PostContactShould()
        {
            contactService = new Mock<IContactService>();
            controller = new ContactsController(contactService.Object);
        }

        [Fact]
        public void ReturnCreated()
        {
            // Arrange
            var contact = new Contact(42, "John", "Doe");

            contactService
                .Setup(mock => mock.Add(contact.FirstName, contact.Surname))
                .Returns(contact);

            // Act
            var result = controller.Post(new ContactRequest(contact.FirstName, contact.Surname));

            // Assert
            var createdResult = result.ShouldBeOfType<CreatedResult>();
            createdResult.Location.ToString().ShouldBe($"/contacts/{contact.Id}");

            var contactResponse = createdResult.Value.ShouldBeOfType<ContactResponse>();
            contactResponse.Id.ShouldNotBe(0);
            contactResponse.FirstName.ShouldBe(contact.FirstName);
            contactResponse.Surname.ShouldBe(contact.Surname);
        }

        [Fact]
        public void ReturnBadRequestGivenMissingFirstName()
        {
            // Arrange
            controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = controller.Post(new ContactRequest(null, "Doe"));

            // Assert
            result.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public void ReturnBadRequestGivenMissingSurname()
        {
            // Arrange
            controller.ModelState.AddModelError("Surname", "Required");

            // Act
            var result = controller.Post(new ContactRequest("John", null));

            // Assert
            result.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public void ReturnBadRequestGivenMissingFirstNameAndSurname()
        {
            // Arrange
            controller.ModelState.AddModelError("FirstName", "Required");
            controller.ModelState.AddModelError("Surname", "Required");

            // Act
            var result = controller.Post(new ContactRequest(null, null));

            // Assert
            result.ShouldBeOfType<BadRequestResult>();
        }
    }
}
