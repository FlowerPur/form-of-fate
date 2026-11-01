using UnityEngine;
using UnityEngine.UI;

public sealed class SaveInfoMenu : Menu
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _deleteButton;

    [SerializeField] private SaveInfoMenuPanel _panel;

    [SerializeField] private SaveSelectMenu _saveSelectMenu;
    [SerializeField] private SaveCreateMenu _saveCreateMenu;

    private SaveData _save;

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

    public void SetSave(SaveData save)
    {
        if (_save.Equals(save)) return;

        _save = save;

        _panel.UpdateUI(_save);
    }

    private void OnBackButtonClick() => SwitchTo(_saveSelectMenu);

    private void OnDeleteButtonClick()
    {
        SaveStorage.Delete(_save.Name);

        if (SaveStorage.LoadAll().Length == 0)
            SwitchTo(_saveCreateMenu);
        else
            SwitchTo(_saveSelectMenu);
    }
}
