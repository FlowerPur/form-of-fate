using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenAI;
using OpenAI.Chat;

public sealed class OpenRouterNeuralNetwork : INeuralNetwork
{
    private const string Url = "https://openrouter.ai/api/v1";

    private readonly ChatClient _client;

    private readonly int _historySize;

    public OpenRouterNeuralNetwork(
        string apiKey,
        string model,
        List<Message> messages,
        int historySize)
    {
        _client = new(
            model: model,
            credential: new System.ClientModel.ApiKeyCredential(apiKey),
            options: new OpenAIClientOptions
            {
                Endpoint = new Uri(Url)
            });

        Messages = messages;
        _historySize = historySize;
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

        await foreach (var response in _client.CompleteChatStreamingAsync(
            GetTrimmedMessages()).ConfigureAwait(false))
        {
            foreach (var content in response.ContentUpdate)
            {
                if (string.IsNullOrEmpty(content.Text))
                    continue;

                fullResponse += content.Text;
                yield return content.Text;
            }
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

        ChatCompletion response = await _client.CompleteChatAsync(
            GetTrimmedMessages()).ConfigureAwait(false);

        string fullResponse = string.Concat(
            response.Content.Select(content => content.Text));

        Messages.Add(new Message
        {
            Role = ChatRole.Assistant,
            Content = fullResponse
        });

        return fullResponse;
    }

    public void Dispose() { }

    private List<ChatMessage> GetTrimmedMessages()
    {
        var systemMessage = Messages.FirstOrDefault(m =>
            m.Role == ChatRole.System);

        var history = Messages
            .Where(m => m.Role != ChatRole.System)
            .TakeLast(_historySize)
            .Select(ToOpenAI)
            .ToList();

        var hasSystemMessage = Messages.Any(m => m.Role == ChatRole.System);

        if (hasSystemMessage)
            history.Insert(0, ToOpenAI(Messages.First(m => m.Role == ChatRole.System)));

        return history;
    }

    private ChatMessage ToOpenAI(Message message) =>
        message.Role switch
        {
            ChatRole.System => new SystemChatMessage(message.Content),
            ChatRole.User => new UserChatMessage(message.Content),
            ChatRole.Assistant => new AssistantChatMessage(message.Content),
            _ => new UserChatMessage(message.Content)
        };
}
