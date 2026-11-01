using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.GenAI;
using Google.GenAI.Types;

public sealed class GoogleNeuralNetwork : INeuralNetwork
{
    private readonly Client _client;
    private readonly GenerateContentConfig _config;
    private readonly string _model;
    private readonly int _historySize;

    public GoogleNeuralNetwork(
        string apiKey,
        string model,
        List<Message> messages,
        int historySize)
    {
        _client = new Client(apiKey: apiKey);

        _model = model;
        Messages = messages;
        _historySize = historySize;

        var systemMessage = Messages.FirstOrDefault(
            message => message.Role == ChatRole.System);

        _config = new()
        {
            SystemInstruction = Messages.Any(message =>
                message.Role == ChatRole.System)
                    ? ToGoogle(Messages.First(message =>
                        message.Role == ChatRole.System))
                    : null
        };
    }

    public List<Message> Messages { get; }

    public async IAsyncEnumerable<string> GetResponseStreamAsync(string message)
    {
        Messages.Add(new Message
        {
            Role = ChatRole.User,
            Content = message
        });

        string fullResponse = string.Empty;

        await foreach (var response in _client.Models.GenerateContentStreamAsync(
            model: _model,
            contents: GetTrimmedMessages(),
            config: _config).ConfigureAwait(false))
        {
            string chunk = response.Text;

            if (string.IsNullOrEmpty(chunk))
                continue;

            fullResponse += chunk;
            yield return chunk;
        }

        Messages.Add(new Message
        {
            Role = ChatRole.Assistant,
            Content = fullResponse
        });
    }

    public async Task<string> GetResponseAsync(string message)
    {
        Messages.Add(new Message
        {
            Role = ChatRole.User,
            Content = message
        });

        var response = await _client.Models.GenerateContentAsync(
            model: _model,
            contents: GetTrimmedMessages(),
            config: _config
        ).ConfigureAwait(false);

        string responseText = response.Text;

        Messages.Add(new Message
        {
            Role = ChatRole.Assistant,
            Content = responseText
        });

        return responseText;
    }

    public void Dispose() => _client.Dispose();

    private List<Content> GetTrimmedMessages() => Messages
        .Where(message => message.Role != ChatRole.System)
        .TakeLast(_historySize)
        .Select(ToGoogle)
        .ToList();

    private Content ToGoogle(Message message) => new()
    {
        Role = message.Role switch
        {
            ChatRole.User => "user",
            ChatRole.Assistant => "model",
            _ => "user"
        },
        Parts = new List<Part>
        {
            new() { Text = message.Content }
        }
    };
}
