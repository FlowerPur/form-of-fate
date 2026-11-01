using UnityEngine;

public abstract class SimpleAnimatorSettings<TAnimationSettings>
    : AnimatorSettings
    where TAnimationSettings : IAnimationSettings
{
    [field: SerializeField] public TAnimationSettings AnimationSettings { get; private set; }
}