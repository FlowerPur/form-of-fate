using UnityEngine;
using UnityEngine.UI;

public sealed class SettingsMenu : Menu
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _saveButton;

    [SerializeField] private SettingsMenuPanel _panel;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private GameMenu _gameMenu;

    private bool _isFromGameMenu;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
        _saveButton.onClick.AddListener(OnSaveButtonClick);

        _panel.SettingsChanged += OnPanelSettingsChanged;

        UpdateSettingsVisibility();
    }

    private void Start() => _panel.Initialize(SettingsStorage.Load());

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClick);
        _saveButton.onClick.RemoveListener(OnSaveButtonClick);

        _panel.SettingsChanged -= OnPanelSettingsChanged;
    }

    public void SetIsFromGameMenu(bool value)
    {
        _isFromGameMenu = value;

        UpdateSettingsVisibility();
    }

    private void OnBackButtonClick()
    {
        if (_isFromGameMenu)
            SwitchTo(_gameMenu);
        else
            SwitchTo(_mainMenu);
    }

    private void OnSaveButtonClick()
    {
        SettingsStorage.Save(_panel.Settings);
        _panel.UpdateOriginalSettings();

        _saveButton.interactable = _panel.HasChanges;
    }

    private void OnPanelSettingsChanged() => _saveButton.interactable = _panel.HasChanges;

    private void UpdateSettingsVisibility() => _panel.UpdateNeuralNetworkSettingsVisibility(!_isFromGameMenu);
}
