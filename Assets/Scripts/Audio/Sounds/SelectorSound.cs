using UnityEngine;

public sealed class SelectorSound : Sound<SelectorState, Selector>
{
    [SerializeField] private AudioClip _changeAudioClip;

    protected override AudioClip GetStateAudioClip(SelectorState state) =>
        state switch
        {
            SelectorState.Change => _changeAudioClip,
            _ => null
        };
}
