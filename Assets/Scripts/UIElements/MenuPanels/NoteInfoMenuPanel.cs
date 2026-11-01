using UnityEngine;
using TMPro;

public sealed class NoteInfoMenuPanel : MenuPanel
{
    [SerializeField] private TextMeshProUGUI _text;

    public void UpdateText(string text) => _text.text = text;
}
