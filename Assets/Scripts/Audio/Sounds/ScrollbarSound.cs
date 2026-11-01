using UnityEngine;

public sealed class ScrollbarSound : Sound<ScrollbarState, ScrollbarStateHandler>
{
    [SerializeField] private AudioClip _hoverAudioClip;
    [SerializeField] private AudioClip _pressedAudioClip;

    protected override AudioClip GetStateAudioClip(ScrollbarState state) =>
        state switch
        {
            ScrollbarState.Hover => _hoverAudioClip,
            ScrollbarState.Pressed => _pressedAudioClip,
            _ => null
        };
}
