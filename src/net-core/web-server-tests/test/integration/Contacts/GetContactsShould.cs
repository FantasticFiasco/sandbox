using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class GetContactsShould : TestBase
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        public async Task ReturnOk(int contactCount)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task ReturnOkGivenNoContacts()
        {
            throw new NotImplementedException();
        }
    }
}
