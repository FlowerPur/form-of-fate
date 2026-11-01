using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class SaveSelectMenu : Menu
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _createButton;

    [SerializeField] private SaveSelectMenuPanel _panel;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SaveCreateMenu _saveCreateMenu;
    [SerializeField] private SaveInfoMenu _saveInfoMenu;
    [SerializeField] private GameLoadingMenu _gameLoadingMenu;

    protected override void Awake()
    {
        base.Awake();

        SaveStorage.SavesChanged += _panel.UpdateSaveSlots;
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
        _createButton.onClick.AddListener(OnCreateButtonClick);

        _panel.InfoRequested += OnInfoRequested;
        _panel.PlayRequested += OnPlayRequested;
    }

    private void Start() => _panel.UpdateSaveSlots(SaveStorage.LoadAll());

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClick);
        _createButton.onClick.RemoveListener(OnCreateButtonClick);

        _panel.InfoRequested -= OnInfoRequested;
        _panel.PlayRequested -= OnPlayRequested;
    }

    private void OnBackButtonClick() => SwitchTo(_mainMenu);

    private void OnCreateButtonClick() => SwitchTo(_saveCreateMenu);

    private void OnInfoRequested(SaveData save)
    {
        _saveInfoMenu.SetSave(save);
        SwitchTo(_saveInfoMenu);
    }

    private void OnPlayRequested(SaveData save)
    {
        _gameLoadingMenu.StartLoading(save);
        SwitchTo(_gameLoadingMenu);
    }
}
