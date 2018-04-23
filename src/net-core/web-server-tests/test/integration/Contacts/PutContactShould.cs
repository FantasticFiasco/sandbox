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
        private readonly IContactService contactService;

        public PutContactShould()
        {
            contactService = Resolve<IContactService>();
        }

        [Fact]
        public async Task ReturnOkGivenUpdatedFirstName()
        {
            // Arrange
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest("Dan", "Doe");

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");

            var updatedContact = await response.Content.ReadAsJsonAsync<ContactResponse>();
            updatedContact.Id.ShouldBe(createdContact.Id);
            updatedContact.FirstName.ShouldBe(updatedContact.FirstName);
            updatedContact.Surname.ShouldBe(updatedContact.Surname);
        }

        [Fact]
        public async Task ReturnOkGivenUpdatedSurname()
        {
            // Arrange
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest("John", "Kane");

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");

            var updatedContact = await response.Content.ReadAsJsonAsync<ContactResponse>();
            updatedContact.Id.ShouldBe(createdContact.Id);
            updatedContact.FirstName.ShouldBe(updatedContact.FirstName);
            updatedContact.Surname.ShouldBe(updatedContact.Surname);
        }

        [Fact]
        public async Task ReturnOkGivenUpdatedFirstNameAndSurname()
        {
            // Arrange
            var createdContact = contactService.Add("John", "Doe");

            var contactToUpdate = new ContactRequest("Dane", "Kane");

            // Act
            var response = await Client.PutAsJsonAsync($"contacts/{createdContact.Id}", contactToUpdate);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");

            var updatedContact = await response.Content.ReadAsJsonAsync<ContactResponse>();
            updatedContact.Id.ShouldBe(createdContact.Id);
            updatedContact.FirstName.ShouldBe(updatedContact.FirstName);
            updatedContact.Surname.ShouldBe(updatedContact.Surname);
        }

        [Fact]
        public async Task ReturnBadRequestGivenMissingFirstName()
        {
            // Arrange
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
