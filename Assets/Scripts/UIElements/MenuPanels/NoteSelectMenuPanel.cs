using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class NoteSelectMenuPanel : MenuPanel
{
    private readonly List<NoteSlot> _noteSlots = new();

    [SerializeField] private Transform _content;

    [SerializeField] private NoteSlot _noteSlotPrefab;

    public event Action<NoteData> InfoRequested;

    protected override void OnEnable()
    {
        base.OnEnable();

        SubscribeNoteSlots();
    }

    private void OnDisable() => UnsubscribeNoteSlots();

    private void OnSlotInfoRequested(NoteData data) => InfoRequested.Invoke(data);

    private void SubscribeNoteSlots()
    {
        foreach (NoteSlot slot in _noteSlots)
            slot.InfoRequested += OnSlotInfoRequested;
    }

    private void UnsubscribeNoteSlots()
    {
        foreach (NoteSlot slot in _noteSlots)
            slot.InfoRequested -= OnSlotInfoRequested;
    }

    public void UpdateNoteSlots(List<NoteData> notes, string searchText)
    {
        UnsubscribeNoteSlots();
        ClearNoteSlots();
        CreateNoteSlots(notes, searchText);
        SubscribeNoteSlots();
    }

    private void ClearNoteSlots()
    {
        foreach (NoteSlot slot in _noteSlots)
            Destroy(slot.gameObject);

        _noteSlots.Clear();
    }

    private void CreateNoteSlots(List<NoteData> notes, string searchText)
    {
        IEnumerable<NoteData> filteredNotes = notes.AsEnumerable().Reverse();

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            filteredNotes = filteredNotes.Where(note =>
                note.Name != null &&
                note.Name.IndexOf(searchText, System.StringComparison.OrdinalIgnoreCase) >= 0);
        }

        int index = 1;

        foreach (NoteData data in filteredNotes)
        {
            NoteSlot slot = Instantiate(_noteSlotPrefab, _content);
            slot.transform.SetSiblingIndex(index++);
            slot.Data = data;

            _noteSlots.Add(slot);
        }
    }
}
