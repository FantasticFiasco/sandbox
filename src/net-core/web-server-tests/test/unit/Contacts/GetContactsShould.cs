using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Unit.Contacts
{
    public class GetContactsShould
    {
        private readonly Mock<IContactService> contactService;
        private readonly ContactsController controller;

        public GetContactsShould()
            : base()
        {
            contactService = new Mock<IContactService>();
            controller = new ContactsController(contactService.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public void ReturnOk(int contactCount)
        {
            // Arrange
            var contacts = Enumerable
                .Range(1, contactCount)
                .Select(id => new Contact(id, $"John {id}", "Doe"))
                .ToArray();

            contactService
                .Setup(mock => mock.GetAll())
                .Returns(contacts);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();

            var contactsResponse = okResult.Value.ShouldBeOfType<ContactResponse[]>();
            contactsResponse.Length.ShouldBe(contactCount);
        }

        [Fact]
        public void ReturnOkGivenNoContacts()
        {
            // Act
            var result = controller.Get();

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();

            var contactsResponse = okResult.Value.ShouldBeOfType<ContactResponse[]>();
            contactsResponse.ShouldBeEmpty();
        }
    }
}
