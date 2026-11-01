using UnityEngine;

public sealed class InputFieldSound : Sound<InputFieldState, InputFieldStateHandler>
{
    [SerializeField] private AudioClip _hoverAudioClip;
    [SerializeField] private AudioClip _pressedAudioClip;
    [SerializeField] private AudioClip _changeAudioClip;

    protected override AudioClip GetStateAudioClip(InputFieldState state) =>
        state switch
        {
            InputFieldState.Hover => _hoverAudioClip,
            InputFieldState.Pressed => _pressedAudioClip,
            InputFieldState.Change => _changeAudioClip,
            _ => null
        };
}
