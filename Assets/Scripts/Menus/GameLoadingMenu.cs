using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class GameLoadingMenu : Menu
{
    [SerializeField] private InformationText _informationText;
    [SerializeField] private Notification _notification;
    [SerializeField] private SaveSelectMenu _saveSelectMenu;
    [SerializeField] private GameMenu _gameMenu;

    private void OnEnable() => _notification.OkButtonClicked += OnNotificationOkButtonClick;

    private void OnDisable() => _notification.OkButtonClicked -= OnNotificationOkButtonClick;

    private void OnNotificationOkButtonClick() => SwitchTo(_saveSelectMenu);

    public async void StartLoading(SaveData data)
    {
        _informationText.SetIndex(0);
        await Task.Delay(1500);

        _informationText.SetIndex(1);
        await Task.Delay(1000);

        if (!await IsNeuralNetworkReadyAsync())
        {
            _notification.Open();
            return;
        }

        _informationText.SetIndex(2);
        await Task.Delay(1000);

        if (data.Messages[^1].Role == ChatRole.System)
        {
            _informationText.SetIndex(3);
            await Task.Delay(1000);

            try
            {
                data.Messages = await InitializeConversationAsync(data.Messages);
            }
            catch
            {
                _notification.Open();
                return;
            }

            _informationText.SetIndex(4);
            await Task.Delay(1000);
        }

        _informationText.SetIndex(5);
        await Task.Delay(1000);

        _gameMenu.SetData(data);
        SwitchTo(_gameMenu);
    }

    private async Task<bool> IsNeuralNetworkReadyAsync()
    {
        try
        {
            var neuralNetwork = NeuralNetworkFactory.Create(new List<Message>());

            return !string.IsNullOrWhiteSpace(await neuralNetwork.GetResponseAsync("Hello"));
        }
        catch
        {
            return false;
        }
    }

    private async Task<List<Message>> InitializeConversationAsync(List<Message> messages)
    {
        var neuralNetwork = NeuralNetworkFactory.Create(messages);

        await neuralNetwork.GetResponseAsync(GetInitialPrompt());

        return neuralNetwork.Messages;
    }

    private string GetInitialPrompt() =>
        SettingsStorage.Load().Language switch
        {
            Language.Auto => Application.systemLanguage switch
            {
                SystemLanguage.English => "I look around and try to figure out where I am. Describe my current location, the surrounding environment, the weather, and the time of day. Also tell me what I have with me, what I am wearing and what physical condition I am in.",
                SystemLanguage.Russian => "Я осматриваюсь и пытаюсь понять, где нахожусь. Опиши моё текущее местоположение, окружающую обстановку, погоду и время суток. Также расскажи, что у меня при себе, во что я одет и в каком физическом состоянии нахожусь.",
                _ => "I look around and try to figure out where I am. Describe my current location, the surrounding environment, the weather, and the time of day. Also tell me what I have with me, what I am wearing and what physical condition I am in."
            },
            Language.English => "I look around and try to figure out where I am. Describe my current location, the surrounding environment, the weather, and the time of day. Also tell me what I have with me, what I am wearing and what physical condition I am in.",
            Language.Russian => "Я осматриваюсь и пытаюсь понять, где нахожусь. Опиши моё текущее местоположение, окружающую обстановку, погоду и время суток. Также расскажи, что у меня при себе, во что я одет и в каком физическом состоянии нахожусь.",
            _ => "I look around and try to figure out where I am. Describe my current location, the surrounding environment, the weather, and the time of day. Also tell me what I have with me, what I am wearing and what physical condition I am in."
        };
}
