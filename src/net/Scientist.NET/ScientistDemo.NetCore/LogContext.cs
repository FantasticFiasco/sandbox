using System;

namespace ScientistDemo.Net
{
    public class LogContext : IDisposable
    {
	    private readonly string contextMessage;

	    public LogContext(string contextMessage)
	    {
		    this.contextMessage = contextMessage;

			Console.WriteLine($"{contextMessage} - Start");
		}

	    public void Dispose()
	    {
			Console.WriteLine($"{contextMessage} - Stop");
		}
    }
}
