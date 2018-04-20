using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Server.Contacts;
using Shouldly;
using Xunit;

namespace Integration.Contacts
{
    public class DeleteContactShould : TestBase
    {
        [Fact]
        public async Task ReturnNoContent()
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
