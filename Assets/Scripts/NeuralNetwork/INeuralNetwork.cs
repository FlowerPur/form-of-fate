using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface INeuralNetwork : IDisposable
{
    public List<Message> Messages { get; }

    public IAsyncEnumerable<string> GetResponseStreamAsync(string message);
    public Task<string> GetResponseAsync(string message);
}
