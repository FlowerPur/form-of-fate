using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class NoteSelectMenu : Menu
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_InputField _searchInputField;

    [SerializeField] private NoteSelectMenuPanel _panel;

    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private NoteInfoMenu _noteInfoMenu;

    private List<NoteData> _notes;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
        _searchInputField.onValueChanged.AddListener(OnSearchInputChanged);

        _panel.InfoRequested += OnInfoRequested;

        _noteInfoMenu.NoteDeleted += OnNoteDeleted;

        _searchInputField.text = string.Empty;
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClick);
        _searchInputField.onValueChanged.RemoveListener(OnSearchInputChanged);

        _panel.InfoRequested -= OnInfoRequested;
    }

    public void SetNotes(List<NoteData> notes)
    {
        _notes = notes;

        _panel.UpdateNoteSlots(_notes, string.Empty);
    }

    private void OnBackButtonClick() => SwitchTo(_gameMenu);

    private void OnSearchInputChanged(string text) => _panel.UpdateNoteSlots(_notes, text);

    private void OnInfoRequested(NoteData note)
    {
        _noteInfoMenu.SetNote(note);
        SwitchTo(_noteInfoMenu);
    }

    private void OnNoteDeleted(NoteData note)
    {
        _notes.Remove(note);

        _panel.UpdateNoteSlots(_notes, string.Empty);
    }
}
