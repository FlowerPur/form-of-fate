using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioVolumeController))]
public abstract class Sound<TState, THandler> : MonoBehaviour
    where TState : Enum
    where THandler : IStateHandler<TState>
{
    private AudioSource _audioSource;

    private THandler _handler;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _handler = GetComponent<THandler>();
    }

    private void OnEnable() => _handler.StateChanged += OnStateChanged;

    private void OnDisable() => _handler.StateChanged -= OnStateChanged;

    protected abstract AudioClip GetStateAudioClip(TState state);

    private void OnStateChanged(TState state)
    {
        AudioClip clip = GetStateAudioClip(state);

        if (clip == null) return;

        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
