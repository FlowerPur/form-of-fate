using System;
using System.Collections.Generic;

public static class NeuralNetworkFactory
{
    public static INeuralNetwork Create(List<Message> messages)
    {
        SettingsData settings = SettingsStorage.Load();

        return settings.Provider switch
        {
            Provider.Ollama => new OllamaNeuralNetwork(settings.OllamaModel, messages, settings.HistorySize),
            Provider.OpenRouter => new OpenRouterNeuralNetwork(settings.OpenRouterApiKey, settings.OpenRouterModel, messages, settings.HistorySize),
            Provider.Google => new GoogleNeuralNetwork(settings.GoogleApiKey, settings.GoogleModel, messages, settings.HistorySize),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
