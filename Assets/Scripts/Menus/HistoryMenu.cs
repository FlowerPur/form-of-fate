using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class HistoryMenu : Menu
{
    [SerializeField] private Button _backButton;

    [SerializeField] private HistoryMenuPanel _panel;

    [SerializeField] private GameMenu _gameMenu;

    private void OnEnable() => _backButton.onClick.AddListener(OnBackButtonCLick);

    private void OnDisable() => _backButton.onClick.RemoveListener(OnBackButtonCLick);

    public void UpdateMessageItems(List<Message> messages) => _panel.UpdateMessageItems(messages);

    private void OnBackButtonCLick() => SwitchTo(_gameMenu);
}
