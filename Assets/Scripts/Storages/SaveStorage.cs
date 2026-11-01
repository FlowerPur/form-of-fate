using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using UnityEngine;

public static class SaveStorage
{
    private static readonly string _path = Path.Combine(Application.persistentDataPath, "Saves");

    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };

    public static event Action<SaveData[]> SavesChanged;

    public static SaveData Load(string name) =>
        JsonSerializer.Deserialize<SaveData>(File.ReadAllText(GetSavePath(name)), _options);

    public static SaveData[] LoadAll() =>
        Directory.GetFiles(_path, "*.json")
            .Select(path => JsonSerializer.Deserialize<SaveData>(File.ReadAllText(path), _options))
            .ToArray();

    public static void Save(SaveData data)
    {
        File.WriteAllText(GetSavePath(data.Name), JsonSerializer.Serialize(data, _options));
        SavesChanged?.Invoke(LoadAll());
    }

    public static void Delete(string name)
    {
        File.Delete(GetSavePath(name));

        SavesChanged?.Invoke(LoadAll());
    }

    public static void EnsureDirectoryExists()
    {
        if (Directory.Exists(_path)) return;

        Directory.CreateDirectory(_path);
    }

    private static string GetSavePath(string name) => Path.Combine(_path, $"{name}.json");
}