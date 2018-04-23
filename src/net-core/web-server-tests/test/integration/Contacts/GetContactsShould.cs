using System.Net;
using System.Threading.Tasks;
using Integration.Extensions;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class GetContactsShould : TestBase
    {
        private readonly IContactService contactService;

        public GetContactsShould()
            : base()
        {
            contactService = Resolve<IContactService>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task ReturnOk(int contactCount)
        {
            // Arrange
            for (int i = 0; i < contactCount; i++)
            {
                contactService.Add($"John {i}", "Doe");
            }

            // Act
            var response = await Client.GetAsync("/contacts");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");

            var contacts = await response.Content.ReadAsJsonAsync<ContactResponse[]>();
            contacts.Length.ShouldBe(contactCount);
        }

        [Fact]
        public async Task ReturnOkGivenNoContacts()
        {
            // Act
            var response = await Client.GetAsync("/contacts");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.ShouldBe("application/json");

            var contacts = await response.Content.ReadAsJsonAsync<ContactResponse[]>();
            contacts.ShouldBeEmpty();
        }
    }
}
