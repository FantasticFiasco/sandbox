using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Server;

namespace Integration
{
    public abstract class TestBase : IDisposable
    {
        protected TestBase()
        {
            Server = new TestServer(Program.Builder(new string[0]));
            Client = Server.CreateClient();
        }

        protected TestServer Server { get; }

        protected HttpClient Client { get; }

        protected T Resolve<T>()
        {
            return (T)Server.Host.Services.GetService(typeof(T));
        }

        public void Dispose()
        {
            Server?.Dispose();
        }
    }
}
