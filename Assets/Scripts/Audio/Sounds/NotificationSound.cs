using UnityEngine;

public sealed class NotificationSound : Sound<NotificationState, Notification>
{
    [SerializeField] private AudioClip _openAudioClip;

    protected override AudioClip GetStateAudioClip(NotificationState state) =>
        state switch
        {
            NotificationState.Open => _openAudioClip,
            _ => null
        };
}
