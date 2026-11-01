using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Slot<TData> : MonoBehaviour
    where TData : ISlotData
{
    [SerializeField] private Button _infoButton;

    [SerializeField] private TextMeshProUGUI _labelText;

    public event Action<TData> InfoRequested;

    public TData Data { get; set; }

    private void Start() => _labelText.text = Data.Name;

    protected virtual void OnEnable() => _infoButton.onClick.AddListener(OnInfoButtonClick);

    protected virtual void OnDisable() => _infoButton.onClick.RemoveListener(OnInfoButtonClick);

    private void OnInfoButtonClick() => InfoRequested.Invoke(Data);
}
