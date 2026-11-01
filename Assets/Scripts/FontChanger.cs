using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class FontChanger : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset _alagard;
    [SerializeField] private TMP_FontAsset _liberationSans;

    private TextMeshProUGUI textMeshProUGUI;

    private void Awake() => textMeshProUGUI = GetComponent<TextMeshProUGUI>();

    private void OnEnable()
    {
        SettingsStorage.SettingsChanged += UpdateFont;

        UpdateFont(SettingsStorage.Load());
    }

    private void UpdateFont(SettingsData settings) => textMeshProUGUI.font = GetFont(settings.Font);

    private TMP_FontAsset GetFont(Font font) =>
        font switch
        {
            Font.Alagard => _alagard,
            Font.LiberationSans => _liberationSans,
            _ => _alagard
        };
}