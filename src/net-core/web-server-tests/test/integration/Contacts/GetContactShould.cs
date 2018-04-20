using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class GetContactShould : TestBase
    {
        [Fact]
        public async Task ReturnOk()
        {
            // Arrange
            var contactService = Resolve<ContactService>();
            var createdContact = contactService.Add("John", "Doe");

            // Act
            var response = await Client.GetAsync($"/contacts/{createdContact.Id}");
            var contact = await response.Content.ReadAsJsonAsync<ContactResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");
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
