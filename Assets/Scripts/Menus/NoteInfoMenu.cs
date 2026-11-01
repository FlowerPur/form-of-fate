using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class NoteInfoMenu : Menu
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _deleteButton;

    [SerializeField] private NoteInfoMenuPanel _panel;

    [SerializeField] private NoteSelectMenu _noteSelectMenu;

    private NoteData _note;

    public event Action<NoteData> NoteDeleted;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
        _deleteButton.onClick.AddListener(OnDeleteButtonClick);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(OnBackButtonClick);
        _deleteButton.onClick.RemoveListener(OnDeleteButtonClick);
    }

    public void SetNote(NoteData note)
    {
        _note = note;

        _panel.UpdateText(_note.Content);
    }

    private void OnBackButtonClick() => SwitchTo(_noteSelectMenu);

    private void OnDeleteButtonClick()
    {
        NoteDeleted.Invoke(_note);

        SwitchTo(_noteSelectMenu);
    }
}
