using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class PostContactShould : TestBase
    {
        [Fact]
        public async Task ReturnCreated()
        {
            // Arrange
            var contact = new ContactRequest("John", "Doe");

            // Act
            var response = await Client.PostAsJsonAsync("/contacts", contact);
            var createdContact = await response.Content.ReadAsJsonAsync<ContactResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");
            response.Headers.Location.ToString().ShouldBe($"/contacts/{createdContact.Id}");
            createdContact.FirstName.ShouldBe(contact.FirstName);
            createdContact.Surname.ShouldBe(contact.Surname);
        }

        [Fact]
        public async Task ReturnBadRequestGivenMissingFirstName()
        {
            // Arrange
            var contact = new ContactRequest(null, "Doe");

            // Act
            var response = await Client.PostAsJsonAsync("/contacts", contact);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnBadRequestGivenMissingSurname()
        {
            // Arrange
            var contact = new ContactRequest("John", null);

            // Act
            var response = await Client.PostAsJsonAsync("/contacts", contact);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }
    }
}
