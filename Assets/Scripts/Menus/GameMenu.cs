using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class GameMenu : Menu
{
    [SerializeField] private Button _saveAndExitButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _historyButton;
    [SerializeField] private Button _notesButton;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private ShiftEnterInputField _shiftEnterInputField;

    [SerializeField] private GameMenuPanel _panel;

    [SerializeField] private Notification _notification;

    [SerializeField] private Music _music;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private HistoryMenu _historyMenu;
    [SerializeField] private NoteSelectMenu _noteSelectMenu;

    private INeuralNetwork _neuralNetwork;

    private SaveData _data;

    private void OnEnable()
    {
        _saveAndExitButton.onClick.AddListener(OnSaveAndExitButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _historyButton.onClick.AddListener(OnHistoryButtonClick);
        _notesButton.onClick.AddListener(OnNotesButtonClick);
        _shiftEnterInputField.SendRequested += OnInputFieldSendRequested;
        _notification.OkButtonClicked += OnNotificationOkButtonClick;

        CommandHandler.NoteCreated += OnNoteCreated;
        CommandHandler.MusicRequested += OnMusicRequested;
    }

    private void OnDisable()
    {
        _saveAndExitButton.onClick.RemoveListener(OnSaveAndExitButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        _historyButton.onClick.RemoveListener(OnHistoryButtonClick);
        _notesButton.onClick.RemoveListener(OnNotesButtonClick);
        _notification.OkButtonClicked -= OnNotificationOkButtonClick;

        CommandHandler.NoteCreated -= OnNoteCreated;
        CommandHandler.MusicRequested -= OnMusicRequested;
    }

    public void SetData(SaveData data)
    {
        _data = data;

        _neuralNetwork = NeuralNetworkFactory.Create(_data.Messages);

        _panel.UpdateText(CommandHandler.RemoveTextInBraces(_data.Messages[^1].Content));

        _music.Play(_data.LastMusicType);
    }

    private void OnSaveAndExitButtonClick()
    {
        SaveData();

        _neuralNetwork.Dispose();
        _neuralNetwork = null;

        SwitchTo(_mainMenu);
    }

    private void OnSettingsButtonClick()
    {
        _settingsMenu.SetIsFromGameMenu(true);

        SwitchTo(_settingsMenu);
    }

    private void OnHistoryButtonClick()
    {
        _historyMenu.UpdateMessageItems(_neuralNetwork.Messages);

        SwitchTo(_historyMenu);
    }

    private void OnNotesButtonClick()
    {
        _noteSelectMenu.SetNotes(_data.Notes);

        SwitchTo(_noteSelectMenu);
    }

    private async void OnInputFieldSendRequested(string text)
    {
        _inputField.text = string.Empty;
        _panel.ResetText();

        try
        {
            string fullResponse = string.Empty;

            await foreach (var chunk in _neuralNetwork.GetResponseStreamAsync(text))
            {
                fullResponse += chunk;

                _panel.UpdateText(CommandHandler.RemoveTextInBraces(fullResponse));
            }

            CommandHandler.ProcessText(fullResponse);
        }
        catch (Exception ex)
        {
            SaveData();

            Debug.LogError($"[GameMenu] Ошибка при генерации: {ex}");

            _notification.Open();
        }
    }

    private void OnNotificationOkButtonClick() => SwitchTo(_mainMenu);

    private void OnNoteCreated(string name, string content) =>
        _data.Notes.Add(new NoteData(name, content));

    private void OnMusicRequested(MusicType type)
    {
        _music.Play(type);
        _data.LastMusicType = type;
    }

    private void SaveData()
    {
        SaveData tempData = _data;

        List<Message> messages = _neuralNetwork.Messages;

        if (messages[^1].Role == ChatRole.User)
            messages.RemoveAt(messages.Count - 1);

        tempData.Messages = messages;
        tempData.SaveDate = DateTime.Now;

        SaveStorage.Save(tempData);
    }
}