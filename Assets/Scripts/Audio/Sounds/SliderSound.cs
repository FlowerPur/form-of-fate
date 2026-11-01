using UnityEngine;

public sealed class SliderSound : Sound<SliderState, SliderStateHandler>
{
    [SerializeField] private AudioClip _hoverAudioClip;
    [SerializeField] private AudioClip _pressedAudioClip;

    protected override AudioClip GetStateAudioClip(SliderState state) =>
        state switch
        {
            SliderState.Hover => _hoverAudioClip,
            SliderState.Pressed => _pressedAudioClip,
            _ => null
        };
}
