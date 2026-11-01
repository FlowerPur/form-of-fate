using UnityEngine;

public sealed class ButtonSound : Sound<ButtonState, ButtonStateHandler>
{
    [SerializeField] private AudioClip _hoverAudioClip;
    [SerializeField] private AudioClip _pressedAudioClip;

    protected override AudioClip GetStateAudioClip(ButtonState state) =>
        state switch
        {
            ButtonState.Hover => _hoverAudioClip,
            ButtonState.Pressed => _pressedAudioClip,
            _ => null
        };
}
