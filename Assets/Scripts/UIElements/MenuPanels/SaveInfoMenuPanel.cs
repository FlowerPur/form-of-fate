using UnityEngine;
using TMPro;

public sealed class SaveInfoMenuPanel : MenuPanel
{
    [SerializeField] private TextMeshProUGUI _nameLabelText;
    [SerializeField] private EnumLabel _difficultyLabel;
    [SerializeField] private TextMeshProUGUI _creationDateLabelText;
    [SerializeField] private TextMeshProUGUI _saveDateLabelText;

    public void UpdateUI(SaveData save)
    {
        _nameLabelText.text = save.Name;
        _difficultyLabel.SetIndex((int)save.Difficulty);
        _creationDateLabelText.text = save.CreationDate.ToString("dd.MM.yyyy HH:mm:ss");
        _saveDateLabelText.text = save.SaveDate.ToString("dd.MM.yyyy HH:mm:ss");
    }
}
