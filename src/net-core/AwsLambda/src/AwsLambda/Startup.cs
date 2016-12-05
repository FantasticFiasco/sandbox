using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace AwsLambda
{
    public class Startup
    {
        public Stream Run()
        {
            var buffer = Encoding.UTF8.GetBytes($"{DateTime.Now} - Hello World!");
            return new MemoryStream(buffer);
        }
    }
}
