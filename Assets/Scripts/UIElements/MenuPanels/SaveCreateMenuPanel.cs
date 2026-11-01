using System;
using UnityEngine;
using TMPro;

public sealed class SaveCreateMenuPanel : MenuPanel
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Selector _difficultySelector;

    private SaveData _save;

    public SaveData Save => _save;
    public bool IsValid => !string.IsNullOrWhiteSpace(_nameInputField.text);

    public event Action SaveChanged;

    protected override void OnEnable()
    {
        base.OnEnable();

        _save = new(_nameInputField.text, (Difficulty)_difficultySelector.Index);

        _nameInputField.text = string.Empty;
        _difficultySelector.SetIndex(1);

        _nameInputField.onValueChanged.AddListener(OnNameInputFieldValueChanged);
        _difficultySelector.IndexChanged += OnDifficultySelectorIndexChanged;
    }

    private void OnDisable()
    {
        _nameInputField.onValueChanged.RemoveListener(OnNameInputFieldValueChanged);
        _difficultySelector.IndexChanged -= OnDifficultySelectorIndexChanged;
    }

    private void OnNameInputFieldValueChanged(string value)
    {
        _save.Name = value;
        SaveChanged.Invoke();
    }

    private void OnDifficultySelectorIndexChanged(int index) => _save.Difficulty = (Difficulty)index;
}
