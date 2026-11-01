using UnityEngine;

public abstract class Localizer<TValue, TComponent> : MonoBehaviour
    where TComponent : MonoBehaviour
{
    [SerializeField] private TValue _english;
    [SerializeField] private TValue _russian;

    protected TComponent Component { get; private set; }

    private void Awake() => Component = GetComponent<TComponent>();

    private void OnEnable() => SettingsStorage.SettingsChanged += UpdateLanguage;

    private void Start() => UpdateLanguage(SettingsStorage.Load());

    private void OnDisable() => SettingsStorage.SettingsChanged -= UpdateLanguage;

    protected abstract void UpdateLanguage(SettingsData settings);

    protected TValue GetLanguage(Language language) =>
        language switch
        {
            Language.Auto => Application.systemLanguage switch
            {
                SystemLanguage.English => _english,
                SystemLanguage.Russian => _russian,
                _ => _english
            },
            Language.English => _english,
            Language.Russian => _russian,
            _ => _english
        };
}
