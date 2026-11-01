using UnityEngine;

[RequireComponent(typeof(InformationText))]
public sealed class InformationTextLocalizer : Localizer<string[], InformationText>
{
    protected override void UpdateLanguage(SettingsData settings) => Component.SetOptions(GetLanguage(settings.Language));
}
