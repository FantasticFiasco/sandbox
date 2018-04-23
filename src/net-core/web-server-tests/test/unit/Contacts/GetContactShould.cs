using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Unit.Contacts
{
    public class GetContactShould
    {
        private readonly Mock<IContactService> contactService;
        private readonly ContactsController controller;

        public GetContactShould()
            : base()
        {
            contactService = new Mock<IContactService>();
            controller = new ContactsController(contactService.Object);
        }

        [Fact]
        public void ReturnOk()
        {
            // Arrange
            var contact = new Contact(42, "John", "Doe");

            contactService
                .Setup(mock => mock.Get(42))
                .Returns(contact);

            // Act
            var result = controller.Get(42);

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();

            var contactResponse = okResult.Value.ShouldBeOfType<ContactResponse>();
            contactResponse.Id.ShouldBe(contact.Id);
            contactResponse.FirstName.ShouldBe(contact.FirstName);
            contactResponse.Surname.ShouldBe(contact.Surname);
        }

        [Fact]
        public void ReturnNotFoundGivenUnknownId()
        {
            // Arrange
            contactService
                .Setup(mock => mock.Get(42))
                .Returns<Contact>(null);

            // Act
            var result = controller.Get(42);

            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}
