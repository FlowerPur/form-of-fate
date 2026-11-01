using System;
using System.IO;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using UnityEngine;

public static class SettingsStorage
{
    private static readonly string _path = Path.Combine(Application.persistentDataPath, "Settings.json");

    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    public static event Action<SettingsData> SettingsChanged;

    public static SettingsData Load() => JsonSerializer.Deserialize<SettingsData>(File.ReadAllText(_path), _options);

    public static void Save(SettingsData settings)
    {
        File.WriteAllText(_path, JsonSerializer.Serialize(settings, _options));

        SettingsChanged?.Invoke(settings);
    }

    public static void EnsureFileExists()
    {
        if (File.Exists(_path)) return;

        Save(SettingsData.GetDefault());
    }
}