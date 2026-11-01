using System;

public abstract class StateAnimator<
    TSettings,
    TState,
    TAnimationSettings,
    THandler,
    TAnimation>
    : BaseAnimator<TSettings, TAnimationSettings, TAnimation>
    where TSettings : StateAnimatorSettings
    where TState : Enum
    where TAnimationSettings : IAnimationSettings
    where THandler : IStateHandler<TState>
    where TAnimation : BaseAnimation<TAnimationSettings>
{
    private THandler _handler;

    protected override void Awake()
    {
        base.Awake();

        _handler = GetComponent<THandler>();
    }

    private void OnEnable() => _handler.StateChanged += OnStateChanged;

    private void OnDisable() => _handler.StateChanged -= OnStateChanged;

    protected abstract TAnimationSettings GetStateAnimationSettings(TState state);

    private void OnStateChanged(TState state)
    {
        Animation.Kill();
        Animation.CreateSequence();
        Animation.InitializeSequence(GetStateAnimationSettings(state));
        Animation.Play();
    }
}