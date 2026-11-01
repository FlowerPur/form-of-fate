using System;

[Serializable]
public struct SettingsData
{
    public int InterfaceSize { get; set; }
    public Font Font { get; set; }
    public Language Language { get; set; }

    public int OverallVolume { get; set; }
    public int SoundVolume { get; set; }
    public int MusicVolume { get; set; }

    public int HistorySize { get; set; }

    public Provider Provider { get; set; }

    public string OllamaModel { get; set; }

    public string OpenRouterApiKey { get; set; }
    public string OpenRouterModel { get; set; }

    public string GoogleApiKey { get; set; }
    public string GoogleModel { get; set; }

    public static SettingsData GetDefault() =>
        new()
        {
            InterfaceSize = 75,
            Font = Font.Alagard,
            Language = Language.Auto,

            OverallVolume = 100,
            SoundVolume = 100,
            MusicVolume = 25,

            HistorySize = 100,

            Provider = Provider.Ollama,

            OllamaModel = string.Empty,

            OpenRouterApiKey = string.Empty,
            OpenRouterModel = string.Empty,

            GoogleApiKey = string.Empty,
            GoogleModel = string.Empty
        };
}