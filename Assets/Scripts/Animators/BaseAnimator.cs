using UnityEngine;

public abstract class BaseAnimator<TSettings, TAnimationSettings, TAnimation>
    : MonoBehaviour
    where TSettings : AnimatorSettings
    where TAnimationSettings : IAnimationSettings
    where TAnimation : BaseAnimation<TAnimationSettings>
{
    public float Progress => Animation.Progress;

    [field: SerializeField] protected TSettings Settings { get; private set; }

    protected TAnimation Animation { get; private set; }

    protected virtual void Awake() => Animation = CreateAnimation();

    protected abstract TAnimation CreateAnimation();
}