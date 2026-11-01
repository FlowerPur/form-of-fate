using System;
using UnityEngine;
using TMPro;

public sealed class SettingsMenuPanel : MenuPanel
{
    [SerializeField] private CustomSlider _interfaceSizeCustomSlider;
    [SerializeField] private Selector _fontSelector;
    [SerializeField] private Selector _languageSelector;
    [SerializeField] private CustomSlider _overallVolumeCustomSlider;
    [SerializeField] private CustomSlider _soundsVolumeCustomSlider;
    [SerializeField] private CustomSlider _musicVolumeCustomSlider;
    [SerializeField] private GameObject _neuralNetworkText;
    [SerializeField] private GameObject _historySizeText;
    [SerializeField] private CustomSlider _historySizeCustomSlider;
    [SerializeField] private GameObject _providerText;
    [SerializeField] private Selector _providerSelector;
    [SerializeField] private GameObject _ollamaModelText;
    [SerializeField] private TMP_InputField _ollamaModelInputField;
    [SerializeField] private GameObject _openRouterApiKeyText;
    [SerializeField] private TMP_InputField _openRouterApiKeyInputField;
    [SerializeField] private GameObject _openRouterModelText;
    [SerializeField] private TMP_InputField _openRouterModelInputField;
    [SerializeField] private GameObject _googleApiKeyText;
    [SerializeField] private TMP_InputField _googleApiKeyInputField;
    [SerializeField] private GameObject _googleModelText;
    [SerializeField] private TMP_InputField _googleModelInputField;

    private bool _areNeuralNetworkSettingsVisible;

    private SettingsData _settings;
    private SettingsData _originalSettings;

    public SettingsData Settings => _settings;
    public bool HasChanges => !_settings.Equals(_originalSettings);

    public event Action SettingsChanged;

    protected override void OnEnable()
    {
        base.OnEnable();

        _interfaceSizeCustomSlider.ValueChanged += OnInterfaceSizeCustomSliderValueChanged;
        _fontSelector.IndexChanged += OnFontSelectorIndexChanged;
        _languageSelector.IndexChanged += OnLanguageSelectorIndexChanged;
        _overallVolumeCustomSlider.ValueChanged += OnOverallVolumeCustomSliderValueChanged;
        _soundsVolumeCustomSlider.ValueChanged += OnSoundsVolumeCustomSliderValueChanged;
        _musicVolumeCustomSlider.ValueChanged += OnMusicVolumeCusromSliderValueChanged;
        _historySizeCustomSlider.ValueChanged += OnHistorySizeCustomSliderValueChanged;
        _providerSelector.IndexChanged += OnProviderSelectorIndexChanged;
        _ollamaModelInputField.onValueChanged.AddListener(OnOllamaModelInputFieldValueChanged);
        _openRouterApiKeyInputField.onValueChanged.AddListener(OnOpenRouterApiKeyInputFieldValueChanged);
        _openRouterModelInputField.onValueChanged.AddListener(OnOpenRouterModelInputFieldValueChanged);
        _googleApiKeyInputField.onValueChanged.AddListener(OnGoogleApiKeyInputFieldValueChanged);
        _googleModelInputField.onValueChanged.AddListener(OnGoogleModelInputFieldValueChanged);
    }

    private void OnDisable()
    {
        _interfaceSizeCustomSlider.ValueChanged -= OnInterfaceSizeCustomSliderValueChanged;
        _fontSelector.IndexChanged -= OnFontSelectorIndexChanged;
        _languageSelector.IndexChanged -= OnLanguageSelectorIndexChanged;
        _overallVolumeCustomSlider.ValueChanged -= OnOverallVolumeCustomSliderValueChanged;
        _soundsVolumeCustomSlider.ValueChanged -= OnSoundsVolumeCustomSliderValueChanged;
        _musicVolumeCustomSlider.ValueChanged -= OnMusicVolumeCusromSliderValueChanged;
        _historySizeCustomSlider.ValueChanged -= OnHistorySizeCustomSliderValueChanged;
        _providerSelector.IndexChanged -= OnProviderSelectorIndexChanged;
        _ollamaModelInputField.onValueChanged.RemoveListener(OnOllamaModelInputFieldValueChanged);
        _openRouterApiKeyInputField.onValueChanged.RemoveListener(OnOpenRouterApiKeyInputFieldValueChanged);
        _openRouterModelInputField.onValueChanged.RemoveListener(OnOpenRouterModelInputFieldValueChanged);
        _googleApiKeyInputField.onValueChanged.RemoveListener(OnGoogleApiKeyInputFieldValueChanged);
        _googleModelInputField.onValueChanged.RemoveListener(OnGoogleModelInputFieldValueChanged);
    }

    public void Initialize(SettingsData settings)
    {
        _originalSettings = settings;
        _settings = _originalSettings;

        InitializeUISettingsValues();
        UpdateProviderUI(_settings.Provider);
    }

    public void UpdateOriginalSettings() => _originalSettings = _settings;

    public void UpdateNeuralNetworkSettingsVisibility(bool isVisible)
    {
        _areNeuralNetworkSettingsVisible = isVisible;

        _neuralNetworkText.SetActive(isVisible);

        _historySizeText.SetActive(isVisible);
        _historySizeCustomSlider.gameObject.SetActive(isVisible);

        _providerText.SetActive(isVisible);
        _providerSelector.gameObject.SetActive(isVisible);

        UpdateProviderUI(_settings.Provider);
    }

    private void OnInterfaceSizeCustomSliderValueChanged(int value)
    {
        _settings.InterfaceSize = value;
        SettingsChanged.Invoke();
    }

    private void OnFontSelectorIndexChanged(int index)
    {
        _settings.Font = (Font)index;
        SettingsChanged.Invoke();
    }

    private void OnLanguageSelectorIndexChanged(int index)
    {
        _settings.Language = (Language)index;
        SettingsChanged.Invoke();
    }

    private void OnOverallVolumeCustomSliderValueChanged(int value)
    {
        _settings.OverallVolume = value;
        SettingsChanged.Invoke();
    }

    private void OnSoundsVolumeCustomSliderValueChanged(int value)
    {
        _settings.SoundVolume = value;
        SettingsChanged.Invoke();
    }

    private void OnMusicVolumeCusromSliderValueChanged(int value)
    {
        _settings.MusicVolume = value;
        SettingsChanged.Invoke();
    }

    private void OnHistorySizeCustomSliderValueChanged(int value)
    {
        _settings.HistorySize = value;
        SettingsChanged.Invoke();
    }

    private void OnProviderSelectorIndexChanged(int index)
    {
        var provider = (Provider)index;

        _settings.Provider = provider;

        UpdateProviderUI(provider);
        SettingsChanged.Invoke();
    }

    private void OnOllamaModelInputFieldValueChanged(string value)
    {
        _settings.OllamaModel = value;
        SettingsChanged.Invoke();
    }

    private void OnOpenRouterApiKeyInputFieldValueChanged(string value)
    {
        _settings.OpenRouterApiKey = value;
        SettingsChanged.Invoke();
    }

    private void OnOpenRouterModelInputFieldValueChanged(string value)
    {
        _settings.OpenRouterModel = value;
        SettingsChanged.Invoke();
    }

    private void OnGoogleApiKeyInputFieldValueChanged(string value)
    {
        _settings.GoogleApiKey = value;
        SettingsChanged.Invoke();
    }

    private void OnGoogleModelInputFieldValueChanged(string value)
    {
        _settings.GoogleModel = value;
        SettingsChanged.Invoke();
    }

    private void InitializeUISettingsValues()
    {
        _interfaceSizeCustomSlider.SetValue(_settings.InterfaceSize);
        _fontSelector.SetIndex((int)_settings.Font);
        _languageSelector.SetIndex((int)_settings.Language);
        _overallVolumeCustomSlider.SetValue(_settings.OverallVolume);
        _musicVolumeCustomSlider.SetValue(_settings.MusicVolume);
        _soundsVolumeCustomSlider.SetValue(_settings.SoundVolume);
        _historySizeCustomSlider.SetValue(_settings.HistorySize);
        _providerSelector.SetIndex((int)_settings.Provider);
        _ollamaModelInputField.text = _settings.OllamaModel;
        _openRouterApiKeyInputField.text = _settings.OpenRouterApiKey;
        _openRouterModelInputField.text = _settings.OpenRouterModel;
        _googleApiKeyInputField.text = _settings.GoogleApiKey;
        _googleModelInputField.text = _settings.GoogleModel;
    }

    private void UpdateProviderUI(Provider provider)
    {
        bool isOllama = provider == Provider.Ollama && _areNeuralNetworkSettingsVisible;

        _ollamaModelText.SetActive(isOllama);
        _ollamaModelInputField.transform.parent.gameObject.SetActive(isOllama);

        bool isOpenRouter = provider == Provider.OpenRouter && _areNeuralNetworkSettingsVisible;

        _openRouterApiKeyText.SetActive(isOpenRouter);
        _openRouterApiKeyInputField.transform.parent.gameObject.SetActive(isOpenRouter);
        _openRouterModelText.SetActive(isOpenRouter);
        _openRouterModelInputField.transform.parent.gameObject.SetActive(isOpenRouter);

        bool isGoogle = provider == Provider.Google && _areNeuralNetworkSettingsVisible;

        _googleApiKeyText.SetActive(isGoogle);
        _googleApiKeyInputField.transform.parent.gameObject.SetActive(isGoogle);
        _googleModelText.SetActive(isGoogle);
        _googleModelInputField.transform.parent.gameObject.SetActive(isGoogle);
    }
}
