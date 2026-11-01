using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using OllamaSharp;

public sealed class OllamaNeuralNetwork : INeuralNetwork
{
    private const string Url = "http://localhost:11434";

    private readonly OllamaApiClient _client;
    private readonly Chat _chat;

    private readonly int _historySize;

    public OllamaNeuralNetwork(string model, List<Message> messages, int historySize)
    {
        _client = new(new Uri(Url))
        {
            SelectedModel = model
        };

        Messages = messages;
        _historySize = historySize;

        _chat = new(_client)
        {
            Messages = GetTrimmedMessages()
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

        _chat.Messages = GetTrimmedMessages();

        string fullResponse = string.Empty;

        await foreach (var chunk in _chat.SendAsync(message))
        {
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

        _chat.Messages = GetTrimmedMessages();

        string fullResponse = string.Empty;

        await foreach (var chunk in _chat.SendAsync(message))
            fullResponse += chunk;

        Messages.Add(new Message
        {
            Role = ChatRole.Assistant,
            Content = fullResponse
        });

        return fullResponse;
    }

    public void Dispose() => _client.Dispose();

    private List<OllamaSharp.Models.Chat.Message> GetTrimmedMessages()
    {
        var messages = Messages.Select(ToOllama).ToList();
        var systemMessage = messages.FirstOrDefault(m =>
            m.Role == OllamaSharp.Models.Chat.ChatRole.System);

        var history = messages
            .Where(m => m.Role != OllamaSharp.Models.Chat.ChatRole.System)
            .TakeLast(_historySize)
            .ToList();

        if (systemMessage != null)
            history.Insert(0, systemMessage);

        return history;
    }

    private OllamaSharp.Models.Chat.Message ToOllama(Message message) => new()
    {
        Role = message.Role switch
        {
            ChatRole.System => OllamaSharp.Models.Chat.ChatRole.System,
            ChatRole.User => OllamaSharp.Models.Chat.ChatRole.User,
            ChatRole.Assistant => OllamaSharp.Models.Chat.ChatRole.Assistant,
            _ => OllamaSharp.Models.Chat.ChatRole.User
        },
        Content = message.Content
    };
}
