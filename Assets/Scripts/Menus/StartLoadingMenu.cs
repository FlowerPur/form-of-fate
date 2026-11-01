using System.Threading.Tasks;
using UnityEngine;

public sealed class StartLoadingMenu : Menu
{
    [SerializeField] private InformationText _informationText;
    [SerializeField] private MainMenu _mainMenu;

    protected async override void Awake()
    {
        base.Awake();

        await Task.Delay(3000);

        _informationText.SetIndex(1);
        await Task.Delay(1000);

        _informationText.SetIndex(2);
        await Task.Delay(1500);

        SwitchTo(_mainMenu);
    }
}