using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class SaveSelectMenuPanel : MenuPanel
{
    private readonly List<SaveSlot> _saveSlots = new();

    [SerializeField] private Transform _content;

    [SerializeField] private SaveSlot _saveSlotPrefab;

    public event Action<SaveData> InfoRequested;
    public event Action<SaveData> PlayRequested;

    protected override void OnEnable()
    {
        base.OnEnable();

        SubscribeSaveSlots();
    }

    private void OnDisable() => UnsubscribeSaveSlots();

    private void OnSlotInfoRequested(SaveData data) => InfoRequested.Invoke(data);

    private void OnSlotPlayRequested(SaveData data) => PlayRequested.Invoke(data);

    private void SubscribeSaveSlots()
    {
        foreach (SaveSlot slot in _saveSlots)
        {
            slot.InfoRequested += OnSlotInfoRequested;
            slot.PlayRequested += OnSlotPlayRequested;
        }
    }

    private void UnsubscribeSaveSlots()
    {
        foreach (SaveSlot slot in _saveSlots)
        {
            slot.InfoRequested -= OnSlotInfoRequested;
            slot.PlayRequested -= OnSlotPlayRequested;
        }
    }

    public void UpdateSaveSlots(SaveData[] saves)
    {
        UnsubscribeSaveSlots();
        ClearSaveSlots();
        CreateSaveSlots(saves);

        if (gameObject.activeInHierarchy)
            SubscribeSaveSlots();
    }

    private void ClearSaveSlots()
    {
        foreach (SaveSlot slot in _saveSlots)
            Destroy(slot.gameObject);

        _saveSlots.Clear();
    }

    private void CreateSaveSlots(SaveData[] saves)
    {
        int index = 1;

        var sortedSaves = saves.OrderByDescending(data => data.SaveDate);

        foreach (SaveData save in sortedSaves)
        {
            SaveSlot slot = Instantiate(_saveSlotPrefab, _content);
            slot.transform.SetSiblingIndex(index++);
            slot.Data = save;

            _saveSlots.Add(slot);
        }
    }
}
