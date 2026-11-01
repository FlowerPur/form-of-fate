using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class InformationText : MonoBehaviour
{
    [SerializeField] private string[] _options;

    private int _index;

    private TextMeshProUGUI _text;

    public void SetOptions(string[] options)
    {
        _options = options;

        UpdateText();
    }

    public void SetIndex(int index)
    {
        _index = index;

        UpdateText();
    }

    public void UpdateText()
    {
        if (_text == null)
            _text = GetComponent<TextMeshProUGUI>();

        _text.text = _options[_index];
    }
}
