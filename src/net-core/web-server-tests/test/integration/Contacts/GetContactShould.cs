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
            throw new NotImplementedException();
        }

        [Fact]
        public async Task ReturnNotFoundGivenUnknownId()
        {
            throw new NotImplementedException();
        }
    }
}
