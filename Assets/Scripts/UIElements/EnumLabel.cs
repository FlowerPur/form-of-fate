using UnityEngine;
using TMPro;

public sealed class EnumLabel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private string[] _options;
    [SerializeField] private int _index;

    public void SetOptions(string[] options)
    {
        _options = options;

        UpdateText();
    }

    public void SetIndex(int index)
    {
        if (_index == index) return;

        _index = index;

        UpdateText();
    }

    private void UpdateText() => _text.text = _options[_index];
}
