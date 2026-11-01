using UnityEngine;
using TMPro;

public sealed class GameMenuPanel : MenuPanel
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _loading;

    public void ResetText()
    {
        _loading.SetActive(true);
        _text.text = string.Empty;
    }

    public void UpdateText(string text)
    {
        _loading.SetActive(false);
        _text.text = text;
    }
}