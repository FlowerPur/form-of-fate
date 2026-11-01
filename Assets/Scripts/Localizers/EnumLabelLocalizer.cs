using UnityEngine;

[RequireComponent(typeof(EnumLabel))]
public sealed class EnumLabelLocalizer : Localizer<string[], EnumLabel>
{
    protected override void UpdateLanguage(SettingsData settings) => Component.SetOptions(GetLanguage(settings.Language));
}
