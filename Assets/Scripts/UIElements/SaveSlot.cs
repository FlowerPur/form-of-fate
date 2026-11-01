using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class SaveSlot : Slot<SaveData>
{
    [SerializeField] private Button _playButton;

    public event Action<SaveData> PlayRequested;

    protected override void OnEnable()
    {
        base.OnEnable();

        _playButton.onClick.AddListener(OnPlayButtonClick);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _playButton.onClick.RemoveListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick() => PlayRequested.Invoke(Data);
}
