using System.Net;
using System.Threading.Tasks;
using Integration.Extensions;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class PutContactShould : TestBase
    {
        [Fact]
        public async Task ReturnOkGivenUpdatedFirstName()
        {
            // Arrange
            var contactService = Resolve<ContactService>();
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest("Dan", "Doe");

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);
            var updatedContact = await response.Content.ReadAsJsonAsync<ContactResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");
            updatedContact.Id.ShouldBe(createdContact.Id);
            updatedContact.FirstName.ShouldBe(updatedContact.FirstName);
            updatedContact.Surname.ShouldBe(updatedContact.Surname);
        }

        [Fact]
        public async Task ReturnOkGivenUpdatedSurname()
        {
            // Arrange
            var contactService = Resolve<ContactService>();
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest("John", "Kane");

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);
            var updatedContact = await response.Content.ReadAsJsonAsync<ContactResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");
            updatedContact.Id.ShouldBe(createdContact.Id);
            updatedContact.FirstName.ShouldBe(updatedContact.FirstName);
            updatedContact.Surname.ShouldBe(updatedContact.Surname);
        }

        [Fact]
        public async Task ReturnOkGivenUpdatedFirstNameAndSurname()
        {
            // Arrange
            var contactService = Resolve<ContactService>();
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest("Dane", "Kane");

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);
            var updatedContact = await response.Content.ReadAsJsonAsync<ContactResponse>();

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");
            updatedContact.Id.ShouldBe(createdContact.Id);
            updatedContact.FirstName.ShouldBe(updatedContact.FirstName);
            updatedContact.Surname.ShouldBe(updatedContact.Surname);
        }

        [Fact]
        public async Task ReturnBadRequestGivenMissingFirstName()
        {
            // Arrange
            var contactService = Resolve<ContactService>();
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest(null, "Doe");

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnBadRequestGivenMissingSurname()
        {
            // Arrange
            var contactService = Resolve<ContactService>();
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest("John", null);

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnBadRequestGivenMissingFirstNameAndSurname()
        {
            // Arrange
            var contactService = Resolve<ContactService>();
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest(null, null);

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task ReturnNotFoundGivenUnknownId()
        {
            // Arrange
            var contactToUpdate = new ContactRequest("John", "Doe");

            // Act
            var response = await Client.PutAsJsonAsync("contacts/42", contactToUpdate);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
