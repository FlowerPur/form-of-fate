using UnityEngine;

public sealed class MenuSound : Sound<MenuState, Menu>
{
    [SerializeField] private AudioClip _openAudioClip;

    protected override AudioClip GetStateAudioClip(MenuState state) =>
        state switch
        {
            MenuState.Open => _openAudioClip,
            _ => null
        };
}
