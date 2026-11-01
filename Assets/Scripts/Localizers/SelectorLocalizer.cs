using UnityEngine;

[RequireComponent(typeof(Selector))]
public sealed class SelectorLocalizer : Localizer<string[], Selector>
{
    protected override void UpdateLanguage(SettingsData settings) => Component.SetOptions(GetLanguage(settings.Language));
}
