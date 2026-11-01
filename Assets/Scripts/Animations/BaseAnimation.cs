using UnityEngine;
using DG.Tweening;

public abstract class BaseAnimation<TSettings>
    where TSettings : IAnimationSettings
{
    public float Progress => Mathf.Clamp01(Sequence.ElapsedPercentage());

    protected Sequence Sequence { get; private set; }

    public abstract void InitializeSequence(TSettings settings);

    public void CreateSequence() => Sequence = DOTween.Sequence().SetAutoKill(false);

    public void Kill() => Sequence?.Kill();

    public void Play() => Sequence.Play();

    public void Restart() => Sequence.Restart();
}