using System.Net;
using System.Threading.Tasks;
using Integration.Extensions;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class GetContactShould : TestBase
    {
        private readonly IContactService contactService;

        public GetContactShould()
        {
            contactService = Resolve<IContactService>();
        }

        [Fact]
        public async Task ReturnOk()
        {
            // Arrange
            var createdContact = contactService.Add("John", "Doe");

            // Act
            var response = await Client.GetAsync($"/contacts/{createdContact.Id}");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");

            var contact = await response.Content.ReadAsJsonAsync<ContactResponse>();
            contact.Id.ShouldBe(createdContact.Id);
            contact.FirstName.ShouldBe(createdContact.FirstName);
            contact.Surname.ShouldBe(createdContact.Surname);
        }

        [Fact]
        public async Task ReturnNotFoundGivenUnknownId()
        {
            // Act
            var response = await Client.GetAsync($"/contacts/42");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
