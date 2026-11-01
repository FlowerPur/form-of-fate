using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class TextLocalizer : Localizer<string, TextMeshProUGUI>
{
    protected override void UpdateLanguage(SettingsData settings) => Component.text = GetLanguage(settings.Language);
}
