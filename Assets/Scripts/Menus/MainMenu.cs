using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenu : Menu
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private SaveSelectMenu _saveSelectMenu;
    [SerializeField] private SaveCreateMenu _saveCreateMenu;
    [SerializeField] private SettingsMenu _settingsMenu;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnPlayButtonClick()
    {
        if (SaveStorage.LoadAll().Length == 0)
            SwitchTo(_saveCreateMenu);
        else
            SwitchTo(_saveSelectMenu);
    }

    private void OnSettingsButtonClick()
    {
        _settingsMenu.SetIsFromGameMenu(false);
        SwitchTo(_settingsMenu);
    }

    private void OnExitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
