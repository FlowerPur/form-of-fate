using UnityEngine;

public sealed class CustomSliderSound : Sound<CustomSliderState, CustomSlider>
{
    [SerializeField] private AudioClip _changeAudioClip;

    protected override AudioClip GetStateAudioClip(CustomSliderState state) =>
        state switch
        {
            CustomSliderState.Change => _changeAudioClip,
            _ => null
        };
}
