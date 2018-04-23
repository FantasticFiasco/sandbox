using System.Net;
using System.Threading.Tasks;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class DeleteContactShould : TestBase
    {
        private readonly IContactService contactService;

        public DeleteContactShould()
        {
            contactService = Resolve<IContactService>();
        }

        [Fact]
        public async Task ReturnNoContent()
        {
            // Arrange
            var createdContact = contactService.Add("John", "Doe");

            // Act
            var response = await Client.DeleteAsync($"contacts/{createdContact.Id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task ReturnNotFoundGivenUnknownId()
        {
            // Act
            var response = await Client.DeleteAsync("contacts/42");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
