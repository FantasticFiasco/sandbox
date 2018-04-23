using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Unit.Contacts
{
    public class PutContactShould
    {
        private readonly Mock<IContactService> contactService;
        private readonly ContactsController controller;

        public PutContactShould()
            : base()
        {
            contactService = new Mock<IContactService>();
            controller = new ContactsController(contactService.Object);
        }

        [Fact]
        public void ReturnOkGivenUpdatedFirstNameAndSurname()
        {
            // Arrange
            var contactRequest = new ContactRequest("Dan", "Kane");

            contactService
                .Setup(mock => mock.Update(42, contactRequest.FirstName, contactRequest.Surname))
                .Returns(new Contact(42, contactRequest.FirstName, contactRequest.Surname));

            // Act
            var result = controller.Put(42, contactRequest);

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();

            var contactResponse = okResult.Value.ShouldBeOfType<ContactResponse>();
            contactResponse.Id.ShouldBe(42);
            contactResponse.FirstName.ShouldBe(contactResponse.FirstName);
            contactResponse.Surname.ShouldBe(contactResponse.Surname);
        }

        [Fact]
        public void ReturnBadRequestGivenMissingFirstName()
        {
            // Arrange
            controller.ModelState.AddModelError("FirstName", "Required");

            var contactRequest = new ContactRequest(null, "Doe");

            // Act
            var result = controller.Put(42, contactRequest);

            // Assert
            result.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public void ReturnBadRequestGivenMissingSurname()
        {
            // Arrange
            controller.ModelState.AddModelError("Surname", "Required");

            var contactRequest = new ContactRequest("John", null);

            // Act
            var result = controller.Put(42, contactRequest);

            // Assert
            result.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public void ReturnBadRequestGivenMissingFirstNameAndSurname()
        {
            // Arrange
            controller.ModelState.AddModelError("FirstName", "Required");
            controller.ModelState.AddModelError("Surname", "Required");

            var contactToUpdate = new ContactRequest(null, null);

            // Act
            var result = controller.Put(42, contactToUpdate);

            // Assert
            result.ShouldBeOfType<BadRequestResult>();
        }
    }
}
