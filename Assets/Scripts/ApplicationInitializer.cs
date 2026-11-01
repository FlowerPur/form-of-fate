using UnityEngine;

public static class ApplicationInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnStart()
    {
        SettingsStorage.EnsureFileExists();
        SaveStorage.EnsureDirectoryExists();
    }
}