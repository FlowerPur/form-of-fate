using System;
using System.Text.RegularExpressions;
using UnityEngine;

public static class CommandHandler
{
    private static readonly Regex NoteRegex = new Regex(
        @"CreateNote\s*\(\s*""(?<title>.*?)""\s*,\s*""(?<content>.*?)""\s*\)",
        RegexOptions.Singleline | RegexOptions.Compiled);

    private static readonly Regex MusicRegex = new Regex(
        @"PlayMusic\s*\(\s*(?<type>\w+)\s*\)",
        RegexOptions.Compiled);

    private static readonly Regex RemoveBracesRegex = new Regex(
        @"\{[^}]*\}|\{.*$",
        RegexOptions.Singleline | RegexOptions.Compiled);

    public static event Action<string, string> NoteCreated;
    public static event Action<MusicType> MusicRequested;

    public static void ProcessText(string inputText)
    {
        if (string.IsNullOrEmpty(inputText))
            return;

        MatchCollection noteMatches = NoteRegex.Matches(inputText);
        foreach (Match match in noteMatches)
        {
            string title = match.Groups["title"].Value;
            string content = match.Groups["content"].Value;

            NoteCreated?.Invoke(title, content);
        }

        MatchCollection musicMatches = MusicRegex.Matches(inputText);
        foreach (Match match in musicMatches)
        {
            string typeStr = match.Groups["type"].Value;

            if (Enum.TryParse(typeStr, true, out MusicType musicType))
                MusicRequested?.Invoke(musicType);
            else
                Debug.LogWarning($"[CommandHandler] Не удалось распарсить тип музыки: {typeStr}");
        }
    }

    public static string RemoveTextInBraces(string text) => RemoveBracesRegex.Replace(text, string.Empty).Trim();
}