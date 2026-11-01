using UnityEngine;
using UnityEngine.UI;

public sealed class SaveCreateMenu : Menu
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _createButton;

    [SerializeField] private SaveCreateMenuPanel _panel;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SaveSelectMenu _saveSelectMenu;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
        _createButton.onClick.AddListener(OnCreateButtonClick);

        _panel.SaveChanged += OnPanelSaveChanged;
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClick);
        _createButton.onClick.RemoveListener(OnCreateButtonClick);

        _panel.SaveChanged -= OnPanelSaveChanged;
    }

    private void OnBackButtonClick()
    {
        if (SaveStorage.LoadAll().Length == 0)
            SwitchTo(_mainMenu);
        else
            SwitchTo(_saveSelectMenu);
    }

    private void OnCreateButtonClick()
    {
        SaveStorage.Save(_panel.Save);

        SwitchTo(_saveSelectMenu);
    }

    private void OnPanelSaveChanged() => _createButton.interactable = _panel.IsValid;
}
